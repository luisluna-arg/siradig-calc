namespace SiradigCalc.ApiFramework.Common;

public static class TypeExtensions
{
    public static IEnumerable<Type> GetAllInterfaces(this Type type)
    {
        return type.GetInterfaces();
    }
}
