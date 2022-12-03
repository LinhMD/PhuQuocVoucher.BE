using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class CartItem : BaseModel, IOrderAble
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public VoucherCompaign VoucherCompaign { get; set; }

    public int VoucherId { get; set; }

    public DateTime? UseDate { get; set; }
    
    
    public int CartId { get; set; }
    public void ConfigOrderBy()
    {
        SetUpOrderBy<CartItem>();
    }
}