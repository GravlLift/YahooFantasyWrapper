using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Xml;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using YahooFantasyWrapper.Client;
using YahooFantasyWrapper.Client.Fantasy;
using YahooFantasyWrapper.Models;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Query.Internal
{
    public class FakeAsyncList<T> : List<T>, IAsyncEnumerable<T>
    {
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            var enumerator = GetEnumerator();
            return AsyncEnumerator.Create(
                moveNextAsync: () => new ValueTask<bool>(enumerator.MoveNext()),
                getCurrent: () => enumerator.Current,
                disposeAsync: () =>
                {
                    enumerator.Dispose();
                    return new ValueTask();
                }
            );
        }
    }

    public class YahooQueryProvider : IAsyncQueryProvider
    {
        private static readonly MethodInfo _createYahooQueryMethod = typeof(YahooQueryProvider).GetRuntimeMethods()
            .Single(m => (m.Name == "CreateYahooQuery") && m.IsGenericMethod);
        private static readonly MethodInfo _genericExecuteMethod = typeof(YahooQueryProvider).GetRuntimeMethods()
            .Single(m => (m.Name == "Execute") && m.IsGenericMethod);

        private readonly HttpClient httpClient;
        private readonly IYahooAuthClient authClient;

        public YahooQueryProvider(HttpClient httpClient, IYahooAuthClient authClient)
        {
            this.httpClient = httpClient;
            this.authClient = authClient;
        }

        public IQueryable CreateQuery(Expression expression) =>
            (IQueryable)_createYahooQueryMethod.MakeGenericMethod(expression.Type.GetSequenceType())
                .Invoke(this, new object[] { expression });

        public IQueryable<TEntity> CreateQuery<TEntity>(Expression expression) =>
            (IQueryable<TEntity>)_createYahooQueryMethod.MakeGenericMethod(typeof(TEntity))
                .Invoke(this, new object[] { expression });

        internal IQueryable<TEntity> CreateYahooQuery<TEntity>(Expression expression)
            where TEntity : IYahooEntity => new YahooSet<TEntity>(this, expression);

        public object Execute(Expression expression) =>
            _genericExecuteMethod.MakeGenericMethod(expression.Type)
                .Invoke(this, new object[] { expression });

        public TResult Execute<TResult>(Expression expression)
        {
            return ExecuteAsync<TResult>(expression).GetAwaiter().GetResult();
        }

        public async Task<TResult> ExecuteAsync<TResult>(
            Expression expression,
            CancellationToken cancellationToken = default
        ) {
            var requestUri = new YahooExpressionVisitor(expression).ToString();
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            // Get auth token for request
            var authToken = await authClient.GetCurrentToken();
            request.Headers.Authorization = new AuthenticationHeaderValue(
                authToken.TokenType,
                authToken.AccessToken
            );

            // Send request
            var taskResponse = httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            );
            using HttpResponseMessage response = await taskResponse.ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var errorMessage = Utils.GetErrorMessage(
                    XDocument.Parse(await response.Content.ReadAsStringAsync())
                );
                throw errorMessage switch
                {
                    "Please provide valid credentials. OAuth oauth_problem=\"unable_to_determine_oauth_type\", realm=\"yahooapis.com\"" => new NoAuthorizationPresentException(),
                    "Please provide valid credentials. OAuth oauth_problem=\"token_expired\", realm=\"yahooapis.com\"" => new ExpiredAuthorizationException(),
                    _ => new GenericYahooException(errorMessage),
                };
            }

            response.EnsureSuccessStatusCode();

            TResult result;

            if (typeof(IEnumerable).IsAssignableFrom(typeof(TResult)))
            {
                var unwrappedType = typeof(TResult).GetGenericArguments()[0];

                // Create serializer of type YahooFantasyXmlSerializer<FakeAsyncList<unwrappedType>>
                var serializer =
                    (XmlSerializer)Activator.CreateInstance(
                        typeof(YahooFantasyXmlSerializer<>).MakeGenericType(
                            typeof(FakeAsyncList<>).MakeGenericType(unwrappedType)
                        )
                    );

                var fantasyContent =
                    await response.Content!.ReadFromXmlAsync(unwrappedType, serializer)
                        .ConfigureAwait(false);

                result = ((FantasyContent<TResult>)fantasyContent).Content;
            }
            else if (typeof(IAsyncEnumerable<>).IsAssignableFromGenericType(typeof(TResult)))
            {
                var unwrappedType = typeof(TResult).GetGenericArguments()[0];

                // Create serializer of type YahooFantasyXmlSerializer<FakeAsyncList<unwrappedType>>
                var serializer =
                    (XmlSerializer)Activator.CreateInstance(
                        typeof(YahooFantasyXmlSerializer<>).MakeGenericType(
                            typeof(FakeAsyncList<>).MakeGenericType(unwrappedType)
                        )
                    );

                dynamic fantasyContent =
                    await response.Content!.ReadFromXmlAsync(unwrappedType, serializer)
                        .ConfigureAwait(false);

                result = fantasyContent.Content;
            }
            else
            {
                var fantasyContent =
                    await response.Content!.ReadFromXmlAsync<FantasyContent<TResult>>(
                            new YahooFantasyXmlSerializer<TResult>()
                        )
                        .ConfigureAwait(false);

                result = fantasyContent.Content;
            }

            return result;
        }
    }
}
