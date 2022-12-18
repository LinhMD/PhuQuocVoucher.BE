namespace PhuQuocVoucher.Business.Dtos.SellerDto;

public class SellerKpiView
{
    public int SellerId { get; set; }

    public Dictionary<int, int> CloseOrderPerMonth { get; set; }

    public Dictionary<int, int> NoOfNewCustomerPerMonth { get; set; }

    public Dictionary<int, long> RevenuesPerMonths { get; set; }
}