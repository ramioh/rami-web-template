namespace Company.Product.Common.ApiCore.DependencyInjection;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class RegisterTypeAttribute : Attribute
{
    public RegisterTypeAttribute(Lifetime lifetime, params Type[] types)
    {
        if (types.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(types), "At least one type is expected.");
        }

        Types = types;
    }

    public Lifetime Lifetime { get; }

    public Type[] Types { get; }
}
