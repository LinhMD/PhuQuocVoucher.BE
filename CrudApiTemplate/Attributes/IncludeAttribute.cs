namespace CrudApiTemplate.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class IncludeAttribute : Attribute
{
    public IncludeAttribute(string path)
    {
        Path = path;
    }

    public string Path { get; }

}