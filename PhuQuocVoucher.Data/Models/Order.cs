using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class Order : BaseModel, IOrderAble
{
    public int Id { get; set; }

    public double TotalPrice { get; set; }

    public DateTime? CompleteDate { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Processing;
    
    public Customer? Customer { get; set; }

    public int? CustomerId { get; set; }
    public Seller? Seller { get; set; }

    public int? SellerId { get; set; }

    public PaymentDetail? PaymentDetail { get; set; }

    public int PaymentDetailId { get; set; }
    
    public Guid? PaymentRequestId { get; set; }

    public IList<QrCode> QrCodes { get; set; }

    public IList<Voucher> Vouchers { get; set; }
    
    public void ConfigOrderBy()
    {
        SetUpOrderBy<Order>();
    }

} 