using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace YahooFantasyWrapper.Query.Internal
{
    public class YahooExpressionVisitor : ExpressionVisitor
    {
        internal List<YahooUrlSegment> Segments = new List<YahooUrlSegment>();

        public YahooExpressionVisitor(Expression expression)
        {
            Segments.Add(new YahooUrlSegment
            {
                ResourceType = expression.Type
            });
            Visit(expression);
        }

        public override string ToString()
        {
            return "/fantasy/v2/" + string.Join("/", Segments);
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(Enumerable.First))
            {
                var filters = new YahooFilterExpressionVisitor(node).Filters;
                var latestSegment = Segments.Last();
                foreach (var filter in filters)
                {
                    if (latestSegment.Modifiers.ContainsKey(filter.Key))
                    {
                        latestSegment.Modifiers[filter.Key].UnionWith(filter.Value);
                    }
                    else
                    {
                        latestSegment.Modifiers.Add(filter.Key, filter.Value);
                    }
                }
            }

            return base.VisitMethodCall(node);
        }

    }
}
