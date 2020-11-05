using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using YahooFantasyWrapper.Client;

namespace YahooFantasyWrapper.Query.Internal
{

    public class YahooExpressionVisitor : ExpressionVisitor
    {
        // Ordered list with a type for key and a boolean for value representing
        // if the type indicate a collection
        private readonly List<(Type type, bool isCollection)> SegmentTypes = new List<(Type type, bool isCollection)>();

        // This confusing nightmare is a grouping of modifiers by type in no particular order,
        // in case a method needs to add modifiers before the segment has been ordered
        private readonly Dictionary<Type, Dictionary<string, HashSet<string>>> SegmentModifiers
            = new Dictionary<Type, Dictionary<string, HashSet<string>>>();

        // By their powers combioned...
        private IEnumerable<YahooUrlSegment> Segments
            => SegmentTypes.Select(kvp
                => new YahooUrlSegment
                {
                    ResourceType = kvp.type,
                    IsCollection = kvp.isCollection,
                    Modifiers = SegmentModifiers[kvp.type]
                });

        private Dictionary<string, HashSet<string>> RootModifiers => SegmentModifiers[SegmentTypes[0].type];
        private void AddSegment(Type type, bool isCollection)
        {
            if (SegmentTypes.Count == 0)
            {
                // Adding root segment
                SegmentTypes.Add((type, isCollection));
            }
            else
            {
                SegmentTypes.Insert(1, (type, isCollection));
            }

            if (!SegmentModifiers.ContainsKey(type))
            {
                SegmentModifiers.Add(type, new Dictionary<string, HashSet<string>>());
            }
        }
        private void AddModifer(Type type, string key, string value)
        {
            if (SegmentModifiers.ContainsKey(type))
            {
                SegmentModifiers[type].AddUnion(key, value);
            }
            else
            {
                SegmentModifiers.Add(type,
                    new Dictionary<string, HashSet<string>> { { key, new HashSet<string> { value } } });
            }
        }

        public YahooExpressionVisitor(Expression expression)
        {
            AddSegment(
                typeof(IEnumerable).IsAssignableFrom(expression.Type)
                    ? expression.Type.GenericTypeArguments[0]
                    : expression.Type,
                expression.Type.IsAssignableFrom(typeof(Models.Response.IYahooCollection)));
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
                    if (RootModifiers.ContainsKey(filter.Key))
                    {
                        RootModifiers[filter.Key].UnionWith(filter.Value);
                    }
                    else
                    {
                        RootModifiers.Add(filter.Key, filter.Value);
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
                        var propertyValue = property.GetValue(constantExpressionFilter.Value);

                        var filterKey = property.GetCustomAttribute<YahooFilterAttribute>()?.Key ??
                            property.Name.ToLowerInvariant();
                        if (propertyValue == default)
                        {
                            continue;
                        }
                        else if (typeof(IEnumerable<>).IsAssignableFrom(property.PropertyType))
                        {
                            throw new NotImplementedException($"Filters with arrays not yet implemented.");
                        }
                        else if (propertyValue is bool booleanValue)
                        {
                            AddModifer(node.Method.ReturnType.GenericTypeArguments[1], filterKey,
                                booleanValue ? "1" : "0");
                        }
                        else
                        {
                            AddModifer(node.Method.ReturnType.GenericTypeArguments[1], filterKey,
                                propertyValue.ToString());
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
                                AddSegment(propertyType.GenericTypeArguments[0], true);
                            }
                            else
                            {
                                AddSegment(propertyType, false);
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
