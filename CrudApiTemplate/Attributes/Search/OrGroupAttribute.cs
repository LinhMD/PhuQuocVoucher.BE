namespace CrudApiTemplate.Attributes.Search;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = true)]
public class OrGroupAttribute : Attribute
{
    public OrGroupAttribute()
    {

    }

    public OrGroupAttribute(string groupName)
    {
        GroupName = groupName;
    }

    public string GroupName { get; init; } = "default";


}