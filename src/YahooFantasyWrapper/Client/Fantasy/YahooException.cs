using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Client
{
    public class GenericYahooException : Exception
    {
        public GenericYahooException(string message)
            : base(message) { }

        public GenericYahooException() { }

        public GenericYahooException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    public class NoAuthorizationPresentException : Exception
    {
        public NoAuthorizationPresentException(string message)
            : base(message) { }

        public NoAuthorizationPresentException(string message, Exception innerException)
            : base(message, innerException) { }

        public NoAuthorizationPresentException() { }
    }
    public class ExpiredAuthorizationException : Exception
    {
        public ExpiredAuthorizationException(string message)
            : base(message) { }

        public ExpiredAuthorizationException(string message, Exception innerException)
            : base(message, innerException) { }

        public ExpiredAuthorizationException() { }
    }
}
