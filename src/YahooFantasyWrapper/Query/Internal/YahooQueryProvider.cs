using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Net.Http.Xml;
using YahooFantasyWrapper.Models.Response;
using System.Threading.Tasks;
using YahooFantasyWrapper.Client;
using System.Net.Http.Headers;
using System.Xml.Linq;
using YahooFantasyWrapper.Models;
using YahooFantasyWrapper.Client.Fantasy;

namespace YahooFantasyWrapper.Query.Internal
{
    public class YahooQueryProvider : IQueryProvider
    {
        private static readonly MethodInfo _createYahooQueryMethod
            = typeof(YahooQueryProvider).GetRuntimeMethods()
                .Single(m => (m.Name == "CreateYahooQuery") && m.IsGenericMethod);
        private static readonly MethodInfo _genericExecuteMethod
            = typeof(YahooQueryProvider).GetRuntimeMethods()
                .Single(m => (m.Name == "Execute") && m.IsGenericMethod);

        private readonly HttpClient httpClient;
        private readonly IYahooAuthClient authClient;

        public YahooQueryProvider(HttpClient httpClient, IYahooAuthClient authClient)
        {
            this.httpClient = httpClient;
            this.authClient = authClient;
        }

        public IQueryable CreateQuery(Expression expression)
            => (IQueryable)_createYahooQueryMethod
                .MakeGenericMethod(expression.Type.GetSequenceType())
                .Invoke(this, new object[] { expression });

        public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression)
            => (IQueryable<TEntity>)_createYahooQueryMethod
                .MakeGenericMethod(typeof(TEntity))
                .Invoke(this, new object[] { expression });

        internal IQueryable<TEntity> CreateYahooQuery<TEntity>(Expression expression)
            where TEntity : IYahooEntity
            => new YahooSet<TEntity>(this, expression);

        public object Execute(Expression expression)
            => _genericExecuteMethod.MakeGenericMethod(expression.Type)
                .Invoke(this, new object[] { expression });

        public TResult Execute<TResult>(Expression expression)
        {
            return ExecuteAsync<TResult>(expression).GetAwaiter().GetResult();
        }

        public async Task<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            var requestUri = new YahooExpressionVisitor(expression).ToString();
            var authToken = await authClient.GetCurrentToken();

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Authorization = new AuthenticationHeaderValue(authToken.TokenType, authToken.AccessToken);

            var taskResponse = httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            using HttpResponseMessage response = await taskResponse.ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var errorMessage = Utils.GetErrorMessage(XDocument.Parse(await response.Content.ReadAsStringAsync()));
                throw errorMessage switch
                {
                    "Please provide valid credentials. OAuth oauth_problem=\"unable_to_determine_oauth_type\", realm=\"yahooapis.com\""
                        => new NoAuthorizationPresentException(),
                    "Please provide valid credentials. OAuth oauth_problem=\"token_expired\", realm=\"yahooapis.com\""
                        => new ExpiredAuthorizationException(),
                    _ => new GenericYahooException(errorMessage),
                };
            }
            response.EnsureSuccessStatusCode();
            // Nullable forgiving reason:
            // GetAsync will usually return Content as not-null.
            // If Content happens to be null, the extension will throw.
            var result = await response.Content!
                .ReadFromXmlAsync<FantasyContent<TResult>>(new YahooFantasyXmlSerializer<TResult>())
                .ConfigureAwait(false);
            return result.Content;
        }
    }
}
