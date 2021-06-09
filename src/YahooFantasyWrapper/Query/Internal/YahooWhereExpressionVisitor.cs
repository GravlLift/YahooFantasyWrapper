using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Serialization;

namespace YahooFantasyWrapper.Query.Internal
{
    internal class YahooWhereExpressionVisitor : ExpressionVisitor
    {
        internal Dictionary<string, HashSet<string>> Filters = new();

        public YahooWhereExpressionVisitor(MethodCallExpression expression)
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

                        if (node.Right is ConstantExpression rightConstantExpression)
                        {
                            Filters.AddUnion(
                                xmlElementAttribute.ElementName,
                                rightConstantExpression.Value.ToString()
                            );
                            return node;
                        }
                        else if (node.Right is MemberExpression rightMemberExpression)
                        {
                            Filters.AddUnion(
                                xmlElementAttribute.ElementName,
                                GetValue(rightMemberExpression).ToString()
                            );
                            return node;
                        }
                    }
                    throw new NotImplementedException($"Equivelency has not been implemented.");
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return base.VisitBinary(node);
                default:
                    throw new NotImplementedException(
                        $"Node type {node.NodeType} has not been implemented."
                    );
            }
        }

        private object GetValue(MemberExpression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));

            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            var getter = getterLambda.Compile();

            return getter();
        }
    }
}
