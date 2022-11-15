using System.Linq.Expressions;

namespace CrudApiTemplate.OrderBy;

//https://asontu.github.io/2020/04/02/a-better-way-to-do-dynamic-orderby-in-c-sharp.html
public class OrderBy<TModel, TBy> : IOrderBy
{
    private readonly Expression<Func<TModel, TBy>> _expression;
	
    public OrderBy(Expression<Func<TModel, TBy>> expression)
    {
        this._expression = expression;
    }

    public dynamic Expression => _expression;

}