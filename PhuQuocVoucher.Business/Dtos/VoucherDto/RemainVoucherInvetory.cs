namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class RemainVoucherInventory
{
    public int RemainInventory { get; set; }

    public DateTime? Date { get; set; }

    public int VoucherId { get; set; }

    public int  LimitPerDay { get; set; }

    public int AlreadyOrder { get; set; }
}