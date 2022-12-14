namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class VoucherKPI
{
    public long Revenue { get; set; }

    public int QrCodeSold { get; set; }

    public long CommissionFee { get; set; }

    public long SellerCommissionFee { get; set; }

    public int VoucherId { get; set; }
}