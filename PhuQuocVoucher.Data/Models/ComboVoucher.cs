namespace PhuQuocVoucher.Data.Models;

public class ComboVoucher
{
    public int  ComboId { get; set; }
    
    public Voucher Combo { get; set; }

    public int VoucherId { get; set; }

    public Voucher Voucher { get; set; }
    
}