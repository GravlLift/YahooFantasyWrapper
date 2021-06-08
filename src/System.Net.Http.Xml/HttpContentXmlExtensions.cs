using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Net.Http.Xml
{
    public static class HttpContentXmlExtensions
    {
        public static Task<object?> ReadFromXmlAsync(this HttpContent content, Type type, XmlSerializer? xmlSerializer = null)
        {
            ValidateContent(content);
            Debug.Assert(content.Headers.ContentType != null);
            Encoding? sourceEncoding = XmlContent.GetEncoding(content.Headers.ContentType.CharSet);

            return ReadFromXmlAsyncCore(content, type, sourceEncoding, xmlSerializer);
        }

        public static Task<T?> ReadFromXmlAsync<T>(this HttpContent content, XmlSerializer? xmlSerializer = null)
        {
            ValidateContent(content);
            Debug.Assert(content.Headers.ContentType != null);
            Encoding? sourceEncoding = XmlContent.GetEncoding(content.Headers.ContentType.CharSet);

            return ReadFromXmlAsyncCore<T>(content, sourceEncoding, xmlSerializer);
        }

        private static async Task<object?> ReadFromXmlAsyncCore(HttpContent content, Type type, Encoding? sourceEncoding, XmlSerializer? xmlSerializer)
        {
            Stream contentStream = await content.ReadAsStreamAsync().ConfigureAwait(false);

            // Wrap content stream into a transcoding stream that buffers the data transcoded from the sourceEncoding to utf-8.
            if (sourceEncoding != null && sourceEncoding != Encoding.UTF8)
            {
                contentStream = new TranscodingReadStream(contentStream, sourceEncoding);
            }

            using (contentStream)
            {
                xmlSerializer ??= new XmlSerializer(type);
                return xmlSerializer.Deserialize(contentStream);
            }
        }

        private static async Task<T?> ReadFromXmlAsyncCore<T>(HttpContent content, Encoding? sourceEncoding, XmlSerializer? xmlSerializer)
        {
            Stream contentStream = await content.ReadAsStreamAsync().ConfigureAwait(false);

            // Wrap content stream into a transcoding stream that buffers the data transcoded from the sourceEncoding to utf-8.
            if (sourceEncoding != null && sourceEncoding != Encoding.UTF8)
            {
                contentStream = new TranscodingReadStream(contentStream, sourceEncoding);
            }

            using (contentStream)
            {
                xmlSerializer ??= new XmlSerializer(typeof(T));
                return (T?)xmlSerializer.Deserialize(contentStream);
            }
        }

        private static void ValidateContent(HttpContent content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            string? mediaType = content.Headers.ContentType?.MediaType;

            if (mediaType == null ||
                !mediaType.Equals(XmlContent.XmlMediaType, StringComparison.OrdinalIgnoreCase) &&
                !IsValidStructuredSyntaxXmlSuffix(mediaType.AsSpan()))
            {
                throw new NotSupportedException("The provided ContentType is not supported; the supported types are 'application/xml' and the structured syntax suffix 'application/+xml'.");
            }
        }

        private static bool IsValidStructuredSyntaxXmlSuffix(ReadOnlySpan<char> mediaType)
        {
            int index = 0;
            int typeLength = mediaType.IndexOf('/');

            ReadOnlySpan<char> type = mediaType.Slice(index, typeLength);
            if (typeLength < 0 ||
                type.CompareTo(XmlContent.XmlType.AsSpan(), StringComparison.OrdinalIgnoreCase) != 0)
            {
                return false;
            }

            index += typeLength + 1;
            int suffixStart = mediaType[index..].IndexOf('+');

            // Empty prefix subtype ("application/+Xml") not allowed.
            if (suffixStart <= 0)
            {
                return false;
            }

            index += suffixStart + 1;
            ReadOnlySpan<char> suffix = mediaType[index..];
            if (suffix.CompareTo(XmlContent.XmlSubtype.AsSpan(), StringComparison.OrdinalIgnoreCase) != 0)
            {
                return false;
            }

            return true;
        }
    }
}
