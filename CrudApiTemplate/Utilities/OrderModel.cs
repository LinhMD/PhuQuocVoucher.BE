using System.Linq.Expressions;

namespace CrudApiTemplate.Utilities;

public class OrderModel<TModel> where TModel: class
{
    private static readonly List<string> ModelProperty = typeof(TModel).GetProperties().Select(p => p.Name).ToList();

    private string? _propertyName = null;
    public string PropertyName
    {
        get => _propertyName ?? throw new InvalidOperationException();

        set
        {
            if (ModelProperty.Contains(value))
            {
                _propertyName = value;
            }
        }
    }

    public bool IsAscending { get; set; }

}