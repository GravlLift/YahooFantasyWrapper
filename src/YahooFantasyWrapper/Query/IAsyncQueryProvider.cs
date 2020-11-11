using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YahooFantasyWrapper.Query
{
    public interface IAsyncQueryProvider : IQueryProvider
    {
        Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default);
    }
}
