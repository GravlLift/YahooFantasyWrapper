using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using YahooFantasyWrapper.Models.Response;

namespace YahooFantasyWrapper.Client
{
    public class GenericYahooException : Exception
    {
        public GenericYahooException(string message) : base(message)
        {
        }
    }

    public class NoAuthorizationPresentException : Exception
    {
    }
    public class ExpiredAuthorizationException : Exception
    {
    }
}