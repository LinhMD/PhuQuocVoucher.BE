namespace PhuQuocVoucher.Business.Dtos.ServiceDto;

public class ServiceKPI
{
    public long Revenue { get; set; }

    public int QrCodeSold { get; set; }

    public long CommissionFee { get; set; }
    
    
    public long SellerCommissionFee { get; set; }

    public int ServiceId { get; set; }
}