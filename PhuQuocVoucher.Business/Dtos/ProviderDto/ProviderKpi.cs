namespace PhuQuocVoucher.Business.Dtos.ProviderDto;

public class ProviderKpi
{
    public long CommissionFee { get; set; }
    public long SellerCommissionFee { get; set; }
    public long ProviderRevenue { get; set; }
    public int OrderSold { get; set; }
    public int QrCodeSold { get; set; }
    public int ProviderId { get; set; }
}