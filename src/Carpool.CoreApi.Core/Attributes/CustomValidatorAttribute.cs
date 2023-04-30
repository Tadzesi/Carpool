namespace Carpool.CoreApi.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CustomValidatorAttribute : Attribute
{
    public CustomValidatorAttribute(Type _)
    { }
}
