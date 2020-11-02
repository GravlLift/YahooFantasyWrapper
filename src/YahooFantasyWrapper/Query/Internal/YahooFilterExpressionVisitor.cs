using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Query.Internal
{
    internal class YahooFilterExpressionVisitor : ExpressionVisitor
    {
        internal Dictionary<string, HashSet<string>> Filters = new Dictionary<string, HashSet<string>>();

        public YahooFilterExpressionVisitor(MethodCallExpression expression)
        {
            VisitMethodCall(expression);
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    if (node.Left is MemberExpression leftExpression)
                    {
                        var xmlElementAttribute = leftExpression.Member.GetCustomAttribute<XmlElementAttribute>();

                        if (node.Right is ConstantExpression rightExpression)
                        {
                            Filters.Add(xmlElementAttribute.ElementName, rightExpression.Value.ToString());
                            return node;
                        }
                    }
                    throw new NotImplementedException($"Equivelency has not been implemented.");
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return base.VisitBinary(node);
                default:
                    throw new NotImplementedException($"Node type {node.NodeType} has not been implemented.");
            }

        }
    }
}
