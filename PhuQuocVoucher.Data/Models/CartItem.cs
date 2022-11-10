namespace PhuQuocVoucher.Data.Models;

public class CartItem : BaseModel
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public Voucher Voucher { get; set; }

    public int VoucherId { get; set; }
    
    public PriceBook? Price { get; set; }
    
    public DateTime? UseDate { get; set; }
    
    public int PriceId { get; set; }
    
    
    public int CartId { get; set; }
}