using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YahooFantasyWrapper.Configuration;
using YahooFantasyWrapper.Models;
using YahooFantasyWrapper.Infrastructure;
using System.Net.Http;
//using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json.Linq;

namespace YahooFantasyWrapper.Client
{
    /// <summary>
    /// Client used to interface with authentication mechanism of Yahoo
    /// </summary>
    public class YahooAuthClient : IYahooAuthClient
    {
        private const string AccessTokenKey = "access_token";
        private const string RefreshTokenKey = "refresh_token";
        private const string ExpiresKey = "expires_in";
        private const string TokenTypeKey = "token_type";
        private readonly HttpClient client;
        private readonly IPersistAuthorizationService persistAuthorizationService;
        private AuthModel auth;

        /// <summary>
        /// Guid to represent user from Yahoo Api
        /// </summary>
        public string UserProfileGUID { get; set; }

        /// <summary>
        /// Client configuration object.
        /// </summary>
        private readonly IOptions<YahooConfiguration> configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="YahooAuthClient"/> class.
        /// </summary>
        /// <param name="client">Http client</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="persistAuthorizationService"></param>
        public YahooAuthClient(
            HttpClient client,
            IOptions<YahooConfiguration> configuration,
            IPersistAuthorizationService persistAuthorizationService)
        {
            this.client = client;
            this.configuration = configuration;
            this.persistAuthorizationService = persistAuthorizationService;
        }


        /// <summary>
        /// It's required to store the User GUID obtained in the response for further usage
        /// https://developer.yahoo.com/oauth2/guide/flows_authcode/
        /// </summary>
        /// <param name="args"></param>
        protected void AfterGetauth(BeforeAfterRequestArgs args)
        {
            //var responseJObject = JsonDocument.Parse(args.Response);
            //UserProfileGUID = responseJObject.RootElement.GetProperty("xoauth_yahoo_guid").GetString();
            var responseJObject = JObject.Parse(args.Response);
            UserProfileGUID = responseJObject.SelectToken("xoauth_yahoo_guid").Value<string>();
        }

        /// <summary>
        /// Obtains user information using OAuth2 service and data provided via callback request.
        /// </summary>
        /// <param name="parameters">Callback request payload (parameters).</param>
        public async Task<UserInfo> GetUserInfo(NameValueCollection parameters)
        {
            CheckErrorAndSetState(parameters);
            await QueryAuth("authorization_code", parameters);
            return await GetUserInfo();
        }
        /// <summary>
        /// Get User Profile Infor from Yahoo Api
        /// https://social.yahooapis.com/v1/user/{UserProfileGUID}/profile?format=json
        /// </summary>
        /// <returns></returns>
        protected async Task<UserInfo> GetUserInfo()
        {
            string url = string.Format(AuthApiEndPoints.UserInfoServiceEndpoint.Resource, UserProfileGUID);
            var tempEndPoint = new EndPoint
            {
                BaseUri = AuthApiEndPoints.UserInfoServiceEndpoint.BaseUri,
                Resource = url
            };

            using (var request = RequestFactory.CreateRequest(tempEndPoint, auth.TokenType, auth.AccessToken))
            {
                var response = await client.GetAsync(request.RequestUri);

                var result = await response.Content.ReadAsStringAsync();
                //var userInfo = JsonSerializer.Deserialize<UserInfo>(result);
                var userInfo = JsonConvert.DeserializeObject<UserInfo>(result);
                return userInfo;
            }
        }

        private void CheckErrorAndSetState(NameValueCollection parameters)
        {
            const string errorFieldName = "error";
            var error = parameters[errorFieldName];
            if (!string.IsNullOrWhiteSpace(error))
            {
                throw new UnexpectedResponseException(errorFieldName);
            }
        }

        /// <summary>
        /// Issues query for access token and parses response.
        /// </summary>
        /// <param name="grantType">'refresh_token' or 'authorization_code'</param>
        /// <param name="parameters">Callback request payload (parameters).</param>
        private async Task QueryAuth(string grantType, NameValueCollection parameters)
        {
            using (var request = RequestFactory.CreateRequest(
                AuthApiEndPoints.authServiceEndpoint,
                HttpMethod.Post))
            {

                var body = new Dictionary<string, string>
                {
                    {"client_id", configuration.Value.ClientId },
                    {"client_secret", configuration.Value.ClientSecret },
                    {"grant_type", grantType }
                };

                if (grantType == "refresh_token")
                {
                    body.Add("refresh_token", parameters.GetOrThrowUnexpectedResponse("refresh_token"));
                }
                else
                {
                    body.Add("code", parameters.GetOrThrowUnexpectedResponse("code"));
                    body.Add("redirect_uri", configuration.Value.RedirectUri);
                }

                request.Content = new FormUrlEncodedContent(body);
                var response = await client.SendAsync(request);

                AfterGetauth(new BeforeAfterRequestArgs
                {
                    Response = await response.Content.ReadAsStringAsync(),
                    Parameters = parameters
                });

                auth = auth ?? new AuthModel();

                auth.AccessToken = ParseTokenResponse(await response.Content.ReadAsStringAsync(), AccessTokenKey);
                if (string.IsNullOrEmpty(auth.AccessToken))
                    throw new UnexpectedResponseException(AccessTokenKey);

                auth.TokenType = ParseTokenResponse(await response.Content.ReadAsStringAsync(), TokenTypeKey);

                if (int.TryParse(ParseTokenResponse(await response.Content.ReadAsStringAsync(), ExpiresKey), out int expiresIn)
                    && DateTimeOffset.TryParse(response.Headers.GetValues("Date").FirstOrDefault(), out DateTimeOffset responseTimeStamp))
                    auth.ExpiresAt = responseTimeStamp.AddSeconds(expiresIn);

                if (grantType != "refresh_token")
                {
                    auth.RefreshToken = ParseTokenResponse(await response.Content.ReadAsStringAsync(), RefreshTokenKey);
                }

                if (persistAuthorizationService != null)
                    await persistAuthorizationService.UpdateAuthModelAsync(auth);
            }
        }

        /// <summary>
        /// Parse Response from Api Call and returns Access Token
        /// </summary>
        /// <param name="response">api response</param>
        /// <param name="key">key for response type</param>
        /// <returns></returns>
        private string ParseTokenResponse(string response, string key)
        {
            if (string.IsNullOrEmpty(response) || string.IsNullOrEmpty(key))
                return null;

            try
            {
                // response can be sent in JSON format
                //var token = JsonDocument.Parse(response).RootElement.GetProperty(key);
                var token = JObject.Parse(response).SelectToken(key);
                return token.ToString();
            }
            catch (JsonException)
            {
                // or it can be in "query string" format (param1=val1&param2=val2)
                var collection = System.Web.HttpUtility.ParseQueryString(response);
                return collection[key];
            }
        }
        /// <summary>
        /// Gets Current Token from request
        /// </summary>
        /// <param name="refreshToken">refresh token used for generation of new access token </param>
        /// <param name="forceUpdate">flag to force generation of new access token</param>
        /// <returns>Access Token</returns>
        public async Task<AuthModel> GetCurrentToken(string refreshToken = null, bool forceUpdate = false)
        {
            if (auth != null && !forceUpdate && auth.IsValid)
            {
                return auth;
            }
            else
            {
                // Refresh from service, maybe another client has updated this key
                if (persistAuthorizationService != null)
                {
                    var newAuth = await persistAuthorizationService.GetAuthModelAsync();
                    auth = newAuth;
                    if (auth != null && newAuth.IsValid)
                    {
                        return auth;
                    }
                }

                NameValueCollection parameters = new NameValueCollection();
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    parameters.Add("refresh_token", refreshToken);
                }
                else if (!string.IsNullOrEmpty(auth?.RefreshToken))
                {
                    parameters.Add("refresh_token", auth.RefreshToken);
                }
                if (parameters.Count > 0)
                {
                    await QueryAuth("refresh_token", parameters);
                    return auth;
                }
            }
            throw new Exception("Token never fetched and refresh token not provided.");
        }

        /// <summary>
        /// Returns URI of service which should be called in order to start authentication process.
        /// This URI should be used for rendering login link.
        /// </summary>
        /// Any additional information that will be posted back by service.
        public string GetLoginLinkUri()
        {
            using (var request = RequestFactory.CreateRequest(AuthApiEndPoints.AccessCodeServiceEndpoint))
            {
                var body = new Dictionary<string, string>
                {
                    {"response_type", "code" },
                    {"client_id", configuration.Value.ClientId},
                    {"client_secret", configuration.Value.ClientSecret },
                    {"redirect_uri", configuration.Value.RedirectUri }
                };

                return AddQueryString(request.RequestUri.ToString(), body);
            }
        }

        /// <summary>
        /// Helper Method to Convert QS Params into stirng
        /// </summary>
        /// <param name="uri">Uri to add params to</param>
        /// <param name="queryString">KVPairs that represent QS params</param>
        /// <returns></returns>
        private static string AddQueryString(
          string uri,
          IEnumerable<KeyValuePair<string, string>> queryString)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (queryString == null)
            {
                throw new ArgumentNullException(nameof(queryString));
            }

            var anchorIndex = uri.IndexOf('#');
            var uriToBeAppended = uri;
            var anchorText = "";
            // If there is an anchor, then the query string must be inserted before its first occurance.
            if (anchorIndex != -1)
            {
                anchorText = uri.Substring(anchorIndex);
                uriToBeAppended = uri.Substring(0, anchorIndex);
            }

            var queryIndex = uriToBeAppended.IndexOf('?');
            var hasQuery = queryIndex != -1;

            var sb = new StringBuilder();
            sb.Append(uriToBeAppended);
            foreach (var parameter in queryString)
            {
                sb.Append(hasQuery ? '&' : '?');
                sb.Append(System.Web.HttpUtility.UrlEncode(parameter.Key));
                sb.Append('=');
                sb.Append(System.Web.HttpUtility.UrlEncode(parameter.Value));
                hasQuery = true;
            }

            sb.Append(anchorText);
            return sb.ToString();
        }

        /// <summary>
        /// Resets Auth for Context, this fires when user logs out
        /// </summary>
        public void ClearAuth()
        {
            auth = null;
        }
    }
}