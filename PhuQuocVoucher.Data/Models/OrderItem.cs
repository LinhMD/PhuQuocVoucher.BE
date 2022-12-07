namespace PhuQuocVoucher.Data.Models;

public class OrderItem
{
    
    public Voucher Voucher { get; set; }

    public int Quantity { get; set; }

    public double SoldPrice { get; set; }

    public IList<QrCode> QrCodes { get; set; }
    
}