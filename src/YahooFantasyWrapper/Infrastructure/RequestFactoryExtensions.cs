using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using YahooFantasyWrapper.Client;

namespace YahooFantasyWrapper.Infrastructure
{
    internal static class RequestFactory
    {
        internal static HttpRequestMessage CreateRequest(EndPoint endpoint)
        {
            return CreateRequest(new Uri(endpoint.Uri), HttpMethod.Get);
        }

        internal static HttpRequestMessage CreateRequest(
            EndPoint endpoint,
            string tokenType,
            string token
        ) {
            return CreateRequest(endpoint, HttpMethod.Get, tokenType, token);
        }

        internal static HttpRequestMessage CreateRequest(EndPoint endpoint, HttpMethod method)
        {
            return CreateRequest(new Uri(endpoint.Uri), method);
        }

        internal static HttpRequestMessage CreateRequest(
            EndPoint endpoint,
            HttpMethod method,
            string tokenType,
            string token
        ) {
            var request = CreateRequest(new Uri(endpoint.Uri), method);
            request.Headers.Authorization = new AuthenticationHeaderValue(tokenType, token);
            return request;
        }

        internal static HttpRequestMessage CreateRequest(Uri uri, HttpMethod method)
        {
            return new HttpRequestMessage { RequestUri = uri, Method = method };
        }
    }
}
