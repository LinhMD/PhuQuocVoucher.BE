using PhuQuocVoucher.Data.Repositories;

namespace PhuQuocVoucher.Data.Models;

public class SellerActivity : BaseModel, IOrderAble
{
    
    public int Id { get; set; }
    
    public Seller Seller { get; set; }
    
    public int SellerId { get; set; }

    
    public int VoucherId { get; set; }
    
    public Voucher Voucher { get; set; }

    public Service Service { get; set; }

    public int ServiceTypeId { get; set; }

    public ServiceType ServiceType { get; set; }

    public int ServiceId { get; set; }

    public int ClickRate { get; set; }

    public int VoucherSold { get; set; }

    public double TotalCommissionFee { get; set; }

    
    public void ConfigOrderBy()
    {
        SetUpOrderBy<SellerActivity>();
    }
}