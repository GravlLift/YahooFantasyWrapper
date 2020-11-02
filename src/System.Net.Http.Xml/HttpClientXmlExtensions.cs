using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Net.Http.Xml
{
    public static partial class HttpClientXmlExtensions
    {
        public static Task<object?> GetFromXmlAsync(this HttpClient client, string? requestUri, Type type, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            Task<HttpResponseMessage> taskResponse = client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            return GetFromXmlAsyncCore(taskResponse, type);
        }

        public static Task<object?> GetFromXmlAsync(this HttpClient client, Uri? requestUri, Type type, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            Task<HttpResponseMessage> taskResponse = client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            return GetFromXmlAsyncCore(taskResponse, type);
        }

        public static Task<TValue> GetFromXmlAsync<TValue>(this HttpClient client, string? requestUri, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            Task<HttpResponseMessage> taskResponse = client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            return GetFromXmlAsyncCore<TValue>(taskResponse);
        }

        public static Task<TValue> GetFromXmlAsync<TValue>(this HttpClient client, Uri? requestUri, CancellationToken cancellationToken = default)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            Task<HttpResponseMessage> taskResponse = client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            return GetFromXmlAsyncCore<TValue>(taskResponse);
        }

        private static async Task<object?> GetFromXmlAsyncCore(Task<HttpResponseMessage> taskResponse, Type type)
        {
            using (HttpResponseMessage response = await taskResponse.ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                // Nullable forgiving reason:
                // GetAsync will usually return Content as not-null.
                // If Content happens to be null, the extension will throw.
                return await response.Content!.ReadFromXmlAsync(type).ConfigureAwait(false);
            }
        }

        private static async Task<T> GetFromXmlAsyncCore<T>(Task<HttpResponseMessage> taskResponse)
        {
            using (HttpResponseMessage response = await taskResponse.ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                // Nullable forgiving reason:
                // GetAsync will usually return Content as not-null.
                // If Content happens to be null, the extension will throw.
                return await response.Content!.ReadFromXmlAsync<T>().ConfigureAwait(false);
            }
        }
    }
}
