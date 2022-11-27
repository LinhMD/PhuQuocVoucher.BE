using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class CartItem : BaseModel, IOrderAble
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public Voucher Voucher { get; set; }

    public int VoucherId { get; set; }
    
    public PriceBook? Price { get; set; }
    
    public DateTime? UseDate { get; set; }
    
    public int PriceId { get; set; }
    
    public int? ProfileId { get; set; }

    public Profile Profile { get; set; }
    
    public int CartId { get; set; }
    public void ConfigOrderBy()
    {
        Expression<Func<CartItem, ModelStatus>> orderByStatus = cartItem => cartItem.Status;
        OrderByProvider<CartItem>.OrderByDic.Add(nameof(Status),orderByStatus);
    }
}