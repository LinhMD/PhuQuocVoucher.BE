namespace PhuQuocVoucher.Data.Models;

public class OrderItem : BaseModel
{
    public int Id { get; set; }
    
    public Voucher Voucher { get; set; }

    public int VoucherId { get; set; }

    public int? ProviderId { get; set; }

    public ServiceProvider? Provider { get; set; }

    public Order Order { get; set; }

    public Seller? Seller { get; set; }

    public int? SellerId { get; set; }
    public int OrderId { get; set; }

    public int Quantity { get; set; }

    public long SoldPrice { get; set; }
    
    public long CommissionFee { get; set; }

    public long ProviderRevenue { get; set; }

    public long SellerCommission { get; set; }

    public IList<QrCode> QrCodes { get; set; }

    public bool IsCombo { get; set; }
}