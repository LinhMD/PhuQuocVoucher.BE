using System.Linq.Expressions;
using CrudApiTemplate.CustomException;

namespace CrudApiTemplate.Utilities;

public class OrderModel<TModel> where TModel: class
{
    private static readonly List<string> ModelProperty = typeof(TModel).GetProperties().Select(p => p.Name).ToList();

    private readonly string? _propertyName;
    public string PropertyName
    {
        get => _propertyName ?? throw new InvalidOperationException();

        init
        {
            if (ModelProperty.Contains(value))
            {
                _propertyName = value;
            }
            else
            {
                throw new ModelValueInvalidException($"Type {typeof(TModel).Name} do not have property {value} available for sorting");
            }
        }
    }

    public bool IsAscending { get; init; }

}