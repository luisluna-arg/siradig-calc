namespace SiradigCalc.ApiFramework.Common;

public static class TypeExtensions
{
    public static IEnumerable<Type> GetAllInterfaces(this Type type)
    {
        return type.GetInterfaces();
    }

    public static int GenericParametersCount(this Type type)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));

        return type.IsGenericType ? type.GetGenericArguments().Length : 0;
    }
}
