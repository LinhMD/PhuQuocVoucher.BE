using CrudApiTemplate.View;
using PhuQuocVoucher.Business.Dtos.QrCodeDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderItemDto;

public class OrderItemView : IView<OrderItem>, IDto
{
    public int Id { get; set; }
    
    public int VoucherId { get; set; }

    public int? ProviderId { get; set; }
    
    public int? SellerId { get; set; }
    
    public int OrderId { get; set; }

    public int Quantity { get; set; }

    public long SoldPrice { get; set; }
    
    public long CommissionFee { get; set; }

    public long ProviderRevenue { get; set; }

    public long SellerCommission { get; set; }

    public IList<QrCodeSimpleView> QrCodes { get; set; }

    public bool IsCombo { get; set; }
    
}