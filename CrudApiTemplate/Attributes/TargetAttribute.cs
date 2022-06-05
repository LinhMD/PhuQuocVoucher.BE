namespace CrudApiTemplate.Attributes;

public class TargetAttribute : Attribute
{

    public TargetAttribute()
    {

    }
    public TargetAttribute(string targetPath)
    {
        TargetPath = targetPath;
    }
    public string TargetPath { get; } = string.Empty;
}