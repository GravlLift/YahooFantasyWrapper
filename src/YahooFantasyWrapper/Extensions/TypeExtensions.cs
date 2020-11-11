using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    internal static class TypeExtensions
    {
        #region https://github.com/dotnet/efcore/blob/f2d01b9a88748781469a231e8544b06b0596753c/src/Shared/SharedTypeExtensions.cs#L163

        public static Type GetSequenceType(this Type type)
        {
            var sequenceType = TryGetSequenceType(type);
            if (sequenceType == null)
            {
                // TODO: Add exception message
                throw new ArgumentException();
            }

            return sequenceType;
        }

        public static Type TryGetSequenceType(this Type type)
            => type.TryGetElementType(typeof(IEnumerable<>))
                ?? type.TryGetElementType(typeof(IAsyncEnumerable<>));

        public static Type TryGetElementType(this Type type, Type interfaceOrBaseType)
        {
            if (type.IsGenericTypeDefinition)
            {
                return null;
            }

            var types = GetGenericTypeImplementations(type, interfaceOrBaseType);

            Type singleImplementation = null;
            foreach (var implementation in types)
            {
                if (singleImplementation == null)
                {
                    singleImplementation = implementation;
                }
                else
                {
                    singleImplementation = null;
                    break;
                }
            }

            return singleImplementation?.GenericTypeArguments.FirstOrDefault();
        }

        public static IEnumerable<Type> GetGenericTypeImplementations(this Type type, Type interfaceOrBaseType)
        {
            var typeInfo = type.GetTypeInfo();
            if (!typeInfo.IsGenericTypeDefinition)
            {
                var baseTypes = interfaceOrBaseType.GetTypeInfo().IsInterface
                    ? typeInfo.ImplementedInterfaces
                    : type.GetBaseTypes();
                foreach (var baseType in baseTypes)
                {
                    if (baseType.IsGenericType
                        && baseType.GetGenericTypeDefinition() == interfaceOrBaseType)
                    {
                        yield return baseType;
                    }
                }

                if (type.IsGenericType
                    && type.GetGenericTypeDefinition() == interfaceOrBaseType)
                {
                    yield return type;
                }
            }
        }

        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            type = type.BaseType;

            while (type != null)
            {
                yield return type;

                type = type.BaseType;
            }
        }

        #endregion

        // https://stackoverflow.com/questions/5461295/using-isassignablefrom-with-open-generic-types
        public static bool IsAssignableFromGenericType(this Type genericType, Type givenType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            var baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableFromGenericType(baseType, genericType);
        }
    }
}
