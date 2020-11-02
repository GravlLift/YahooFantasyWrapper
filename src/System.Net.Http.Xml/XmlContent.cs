using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace System.Net.Http.Xml
{
    public sealed partial class XmlContent : HttpContent
    {
        internal const string XmlMediaType = "application/xml";
        internal const string XmlType = "application";
        internal const string XmlSubtype = "xml";
        private static MediaTypeHeaderValue DefaultMediaType
            => new MediaTypeHeaderValue(XmlMediaType) { CharSet = "utf-8" };

        //internal static readonly XmlSerializerOptions s_defaultSerializerOptions
        //    = new XmlSerializerOptions { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = XmlNamingPolicy.CamelCase };

        private readonly XmlSerializer XmlSerializer;
        public Type ObjectType { get; }
        public object? Value { get; }

        private XmlContent(object? inputValue, Type inputType, MediaTypeHeaderValue? mediaType/*, XmlSerializerNamespaces? namespaces*/)
        {
            if (inputType == null)
            {
                throw new ArgumentNullException(nameof(inputType));
            }

            if (inputValue != null && !inputType.IsAssignableFrom(inputValue.GetType()))
            {
                throw new ArgumentException(string.Format("The specified type {0} must derive from the specific value's type {1}.", inputType, inputValue.GetType()));
            }

            Value = inputValue;
            ObjectType = inputType;
            Headers.ContentType = mediaType ?? DefaultMediaType;
            XmlSerializer = new XmlSerializer(inputType);
        }

        public static XmlContent Create<T>(T inputValue, MediaTypeHeaderValue? mediaType = null /*, XmlSerializerNamespaces? namespaces = null*/)
            => Create(inputValue, typeof(T), mediaType/*, options*/);

        public static XmlContent Create(object? inputValue, Type inputType, MediaTypeHeaderValue? mediaType = null /*, XmlSerializerNamespaces? namespaces = null*/)
            => new XmlContent(inputValue, inputType, mediaType/*, options*/);

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context)
            => SerializeToStreamAsyncCore(stream, CancellationToken.None);

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return false;
        }

        private async Task SerializeToStreamAsyncCore(Stream targetStream, CancellationToken cancellationToken)
        {
            Encoding? targetEncoding = GetEncoding(Headers.ContentType?.CharSet);

            // Wrap provided stream into a transcoding stream that buffers the data transcoded from utf-8 to the targetEncoding.
            if (targetEncoding != null && targetEncoding != Encoding.UTF8)
            {
                using TranscodingWriteStream transcodingStream = new TranscodingWriteStream(targetStream, targetEncoding);
                XmlSerializer.Serialize(transcodingStream, Value);
                // The transcoding streams use Encoders and Decoders that have internal buffers. We need to flush these
                // when there is no more data to be written. Stream.FlushAsync isn't suitable since it's
                // acceptable to Flush a Stream (multiple times) prior to completion.
                await transcodingStream.FinalWriteAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                XmlSerializer.Serialize(targetStream, Value);
            }
        }

        internal static Encoding? GetEncoding(string? charset)
        {
            Encoding? encoding = null;

            if (charset != null)
            {
                try
                {
                    // Remove at most a single set of quotes.
                    if (charset.Length > 2 && charset[0] == '\"' && charset[charset.Length - 1] == '\"')
                    {
                        encoding = Encoding.GetEncoding(charset.Substring(1, charset.Length - 2));
                    }
                    else
                    {
                        encoding = Encoding.GetEncoding(charset);
                    }
                }
                catch (ArgumentException e)
                {
                    throw new InvalidOperationException("The character set provided in ContentType is invalid.", e);
                }

                Debug.Assert(encoding != null);
            }

            return encoding;
        }
    }
}
