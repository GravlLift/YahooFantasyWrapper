using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using YahooFantasyWrapper.Client.Fantasy;
using YahooFantasyWrapper.Infrastructure;
using YahooFantasyWrapper.Models;

namespace YahooFantasyWrapper.Client
{
    internal static class Utils
    {
        internal static async Task PostCollection<T>(HttpClient client, EndPoint endpoint, AuthModel auth, T requestBody)
        {
            using var memoryStream = new MemoryStream();
            using var xmlw = XmlWriter.Create(memoryStream);
            var xmlSerializer = new YahooFantasyXmlSerializer<T>();
            xmlSerializer.Serialize(xmlw, requestBody);
            xmlw.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);

            var request = RequestFactory.CreateRequest(endpoint, HttpMethod.Post, auth.TokenType, auth.AccessToken);
            request.Content = new StreamContent(memoryStream);
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml");

            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    GetErrorMessage(
                        XDocument.Parse(await response.Content.ReadAsStringAsync())));
            }
        }

        public static string GetErrorMessage(XDocument xml)
        {
            var result =
                from e in xml.Root.Elements()
                where e.Name.LocalName == "description"
                select e.Value;

            return result.FirstOrDefault() ?? "Unknown XML";
        }

    }

}
