using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace YahooFantasyWrapper.Query.Internal
{
    public class YahooExpressionVisitor : ExpressionVisitor
    {
        internal List<YahooUrlSegment> Segments = new List<YahooUrlSegment>();
        private YahooUrlSegment CurrentSegment => Segments.Last();

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
                var filters = new YahooWhereExpressionVisitor(node).Filters;
                foreach (var filter in filters)
                {
                    if (CurrentSegment.Modifiers.ContainsKey(filter.Key))
                    {
                        CurrentSegment.Modifiers[filter.Key].UnionWith(filter.Value);
                    }
                    else
                    {
                        CurrentSegment.Modifiers.Add(filter.Key, filter.Value);
                    }
                }
                return node;
            }
            else if (node.Method.Name == nameof(IQueryableExtensions.Filter))
            {
                var filter = node.Arguments[1];
                if (filter is ConstantExpression constantExpressionFilter && constantExpressionFilter.Value != null)
                {
                    Type filterType = constantExpressionFilter.Value.GetType();
                    foreach (var property in filterType.GetRuntimeProperties())
                    {
                        var filterValue = property.GetValue(constantExpressionFilter.Value);
                        // TODO: Parse key from attribute or something
                        var filterKey = property.Name.ToLowerInvariant();
                        if (filterValue == default)
                        {
                            continue;
                        }
                        else if (typeof(IEnumerable<>).IsAssignableFrom(filterValue.GetType()))
                        {
                            throw new NotImplementedException($"Filters with arrays not yet implemented.");
                        }
                        else
                        {
                            CurrentSegment.Modifiers.Add(filterKey, filterValue.ToString());
                        }
                    }
                }
                else
                {
                    throw new NotImplementedException($"Filter is of unhandled type {filter.Type.Name}");
                }
                return node;
            }

            return base.VisitMethodCall(node);
        }

    }
}
