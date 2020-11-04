using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace YahooFantasyWrapper.Query.Internal
{
    public class YahooExpressionVisitor : ExpressionVisitor
    {
        internal List<YahooUrlSegment> Segments = new List<YahooUrlSegment>();
        private YahooUrlSegment RootSegment => Segments.First();
        private YahooUrlSegment CurrentSegment => Segments.Last();

        public YahooExpressionVisitor(Expression expression)
        {
            Segments.Add(new YahooUrlSegment
            {
                ResourceType = typeof(IEnumerable).IsAssignableFrom(expression.Type) ?
                    expression.Type.GenericTypeArguments[0] :
                    expression.Type,
                IsCollection = expression.Type.IsAssignableFrom(typeof(Models.Response.IYahooCollection))
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
            if (node.Method.Name == nameof(Enumerable.Where) ||
                node.Method.Name == nameof(Enumerable.First))
            {
                var filters = new YahooWhereExpressionVisitor(node).Filters;
                foreach (var filter in filters)
                {
                    if (RootSegment.Modifiers.ContainsKey(filter.Key))
                    {
                        RootSegment.Modifiers[filter.Key].UnionWith(filter.Value);
                    }
                    else
                    {
                        RootSegment.Modifiers.Add(filter.Key, filter.Value);
                    }
                }
            }
            else if (node.Method.Name == nameof(QueryableExtensions.Filter))
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
            }
            else if (node.Method.Name == nameof(QueryableExtensions.SubResource))
            {
                var expressionArgument = node.Arguments[1];
                if (expressionArgument is UnaryExpression unaryExpression)
                {
                    if (unaryExpression.Operand is LambdaExpression lambdaExpression)
                    {
                        if (lambdaExpression.Body is MemberExpression memberExpression)
                        {
                            var propertyType = ((PropertyInfo)memberExpression.Member).PropertyType;
                            if (typeof(IEnumerable).IsAssignableFrom(propertyType))
                            {
                                Segments.Add(new YahooUrlSegment
                                {
                                    ResourceType = propertyType.GenericTypeArguments[0],
                                    IsCollection = true
                                });
                            }
                            else
                            {
                                Segments.Add(new YahooUrlSegment
                                {
                                    ResourceType = propertyType,
                                    IsCollection = false
                                });
                            }
                        }
                        else
                        {
                            throw new NotImplementedException($"Lambda expression is of unhandled type {unaryExpression.Type.Name}");
                        }
                    }
                    else
                    {
                        throw new NotImplementedException($"Unary operand is of unhandled type {unaryExpression.Type.Name}");
                    }
                }
                else
                {
                    throw new NotImplementedException($"Argument is of unhandled type {expressionArgument.Type.Name}");
                }
            }
            return base.VisitMethodCall(node);
        }

    }
}
