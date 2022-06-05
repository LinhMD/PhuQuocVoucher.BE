﻿using System.Linq.Expressions;
using CrudApiTemplate.Request;

namespace CrudApiTemplate.Utilities;

public class OrderRequest<TModel> : IOrderRequest<TModel> where TModel: class
{
    public IList<OrderModel<TModel>> OrderModels { get; set; } = new List<OrderModel<TModel>>();

    public PagingRequest PagingRequest { get; set; }

    public PagingRequest GetPaging()
    {
        return PagingRequest;
    }


}