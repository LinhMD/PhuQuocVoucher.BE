namespace CrudApiTemplate.Attributes;

[AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class ModelPathAttribute : Attribute
{
    public ModelPathAttribute(string path)
    {
        Path = path;
    }

    public string Path { get; }
}