using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YahooFantasyWrapper.Models;

namespace YahooFantasyWrapper.Client
{
    public interface IYahooFantasyClient
    {
        TransactionsCollectionManager TransactionsCollectionManager { get; }
    }
}
