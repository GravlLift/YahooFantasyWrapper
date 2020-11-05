using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace YahooFantasyWrapper.Models.Response
{
    public interface IYahooEntity
    {

    }

    public interface IYahooCollection : IYahooEntity
    {
    }

    public interface IYahooResource : IYahooEntity
    {

    }

    public interface IYahooQueryable<TYahooCollection>
        : IQueryable<TYahooCollection>
        where TYahooCollection : IYahooEntity
    {

    }

    public interface IYahooQueryable<TYahooCollection, TYahooSubResource>
        : IYahooQueryable<TYahooCollection>
        where TYahooCollection : IYahooEntity
        where TYahooSubResource : IYahooEntity
    {

    }
}
