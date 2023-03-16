using System.Reflection;

namespace System.Linq.Expressions;

public static class ReflectionExtensions
{
    public static PropertyInfo GetPropertyInfo<T, P>(this Expression<Func<T, P>> property)
    {
        if (property is null)
            throw new ArgumentNullException(nameof(property));

        return property.Body switch
        {
            UnaryExpression { Operand: MemberExpression memberExp } => (PropertyInfo) memberExp.Member,
            MemberExpression memberExp => (PropertyInfo) memberExp.Member,
            _ => throw new ArgumentException($"The expression doesn't indicate a valid property. [ {property} ]")
        };
    }
}