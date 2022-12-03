namespace PhuQuocVoucher.Data.Models;

public class TagVoucher
{
    public int TagId { get; set; }

    public Tag Tag { get; set; }
    
    public int VoucherId { get; set; }

    public VoucherCompaign Compaign { get; set; }
}