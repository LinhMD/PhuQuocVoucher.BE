namespace CrudApiTemplate.OrderBy;

public class OrderByProvider<TModel> where TModel: class
{
    public static Dictionary<string, dynamic> OrderByDic = new();
}