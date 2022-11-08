namespace PhuQuocVoucher.Data.Models;

public class OrderItem  : BaseModel
{
     public int Id { get; set;}
 
     public int OrderId { get; set; }
 
     public Order Order { get; set; }

     public Voucher Voucher { get; set; }

     public int VoucherId { get; set; }
     public Profile? Profile { get; set; }
 
     public int? ProfileId { get; set; }
     public Review? Review { get; set; }
 
     public DateTime? UseDate { get; set; }
 
     public PriceBook Price { get; set; }

     public double SoldPrice { get; set; }
     
     public QrCodeInfo? QrCode { get; set; }
     
     public int? QrCodeId { get; set; }
     
     public int PriceId { get; set; }
     
     public int? SellerId { get; set; }
     
     public Seller? Seller { get; set; }

     public double SellerRate { get; set; }
     
     public int ProviderId { get; set; }
     
     public ServiceProvider Provider { get; set; }
     
     public double ProviderRate { get; set; }
}