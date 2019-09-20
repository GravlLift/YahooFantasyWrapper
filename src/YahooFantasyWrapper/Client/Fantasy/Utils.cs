using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using YahooFantasyWrapper.Client.Fantasy;
using YahooFantasyWrapper.Infrastructure;
using YahooFantasyWrapper.Models;

namespace YahooFantasyWrapper.Client
{
    internal static class Utils
    {
        private static async Task<HttpContent> SendRequest(HttpClient client, HttpMethod method, EndPoint endpoint, AuthModel auth = null)
        {
            HttpRequestMessage request;
            if (auth == null)
            {
                request = RequestFactory.CreateRequest(endpoint, method);
            }
            else
            {
                request = RequestFactory.CreateRequest(endpoint, method, auth.TokenType, auth.AccessToken);
            }

            var response = await client.SendAsync(request).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return response.Content;
            }
            else
            {
                var errorMessage = GetErrorMessage(XDocument.Parse(await response.Content.ReadAsStringAsync()));
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    switch (errorMessage)
                    {
                        case "Please provide valid credentials. OAuth oauth_problem=\"unable_to_determine_oauth_type\", realm=\"yahooapis.com\"":
                            throw new NoAuthorizationPresentException();
                        case "Please provide valid credentials. OAuth oauth_problem=\"token_expired\", realm=\"yahooapis.com\"":
                            throw new ExpiredAuthorizationException();
                        default:
                            throw new GenericYahooException(errorMessage);
                    }
                }
                else
                {
                    throw new GenericYahooException(errorMessage);
                }
            }
        }

        /// <summary>
        /// Gets Access Token and makes Request against Endpoint passed in
        /// </summary>
        /// <param name="client">Http client</param>
        /// <param name="endpoint">Uri of Api to Query</param>
        /// <param name="auth">Authorization tokens</param>
        /// <returns></returns>
        internal static async Task<XDocument> GetResponseData(HttpClient client, EndPoint endpoint, AuthModel auth)
        {
            var responseContent = await SendRequest(client, HttpMethod.Get, endpoint, auth);
            var result = await responseContent.ReadAsStringAsync();

            if (string.IsNullOrEmpty(result))
            {
                throw new Exception("Combination of Resource and SubResources not allowed, please try altering");
            }

            return XDocument.Parse(result);
        }

        /// <summary>
        /// Generic Handler to Retrieve Collection for Api
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client">Http client</param>
        /// <param name="endPoint">EndPoint requested</param>
        /// <param name="auth">Authorization tokens</param>
        /// <param name="lookup">Collection Type to Retrieve</param>
        /// <returns></returns>
        internal static async Task<List<T>> GetCollection<T>(HttpClient client, EndPoint endPoint, AuthModel auth, string lookup)
        {
            var xml = await GetResponseData(client, endPoint, auth);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            List<XElement> xElements = xml.Descendants(YahooXml.XMLNS + lookup).ToList();
            List<T> collection = new List<T>();
            foreach (var element in xElements)
            {
                collection.Add((T)serializer.Deserialize(element.CreateReader()));
            }

            return collection;
        }

        /// <summary>
        /// Generic Handler to Retrieve Resource for Api
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client">Http client</param>
        /// <param name="endPoint">EndPoint requested</param>
        /// <param name="auth">Authorization tokens</param>
        /// <param name="lookup">Resource Type to Retrieve</param>
        /// <returns></returns>
        internal static async Task<T> GetResource<T>(HttpClient client, EndPoint endPoint, AuthModel auth, string lookup)
        {
            var xml = await GetResponseData(client, endPoint, auth);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XElement xElement = xml.Descendants(YahooXml.XMLNS + lookup).FirstOrDefault();
            if (xElement == null && IsError(xml))
                throw new InvalidOperationException(GetErrorMessage(xml));
            if (xElement == null)
                throw new InvalidOperationException($"Invalid XML returned. {xml}");

            var resource = (T)serializer.Deserialize(xElement.CreateReader());
            return resource;
        }

        internal static async Task PostCollection<T>(HttpClient client, EndPoint endpoint, AuthModel auth, T requestBody)
        {
            using (var memoryStream = new MemoryStream())
            using (var xmlw = XmlWriter.Create(memoryStream))
            {
                var xmlSerializer = new YahooFantasyXmlSerializer<T>();
                xmlSerializer.Serialize(xmlw, requestBody);
                xmlw.Flush();
                memoryStream.Seek(0, SeekOrigin.Begin);

                var request = RequestFactory.CreateRequest(endpoint, HttpMethod.Post, auth.TokenType, auth.AccessToken);
                request.Content = new StreamContent(memoryStream);
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml");

                await client.SendAsync(request);
            }
        }

        private static string GetErrorMessage(XDocument xml)
        {
            var result =
                from e in xml.Root.Elements()
                where e.Name.LocalName == "description"
                select e.Value;

            return result.FirstOrDefault() ?? "Unknown XML";
        }

        private static bool IsError(XDocument xml)
        {
            return string.Equals(xml.Root.Name.LocalName, "error", StringComparison.OrdinalIgnoreCase);
        }

    }

}
