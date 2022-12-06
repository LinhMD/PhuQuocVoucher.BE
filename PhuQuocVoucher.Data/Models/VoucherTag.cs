namespace PhuQuocVoucher.Data.Models;

public class VoucherTag
{
    public int TagId { get; set; }

    public Tag Tag { get; set; }
    
    public int VoucherId { get; set; }

    public Voucher voucher { get; set; }
}