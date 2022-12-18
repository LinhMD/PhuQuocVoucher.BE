using System.ComponentModel.DataAnnotations;

namespace PhuQuocVoucher.Data.Models;

public class SellerRank : BaseModel
{
    public int Id { get; set; }

    public string Logo { get; set; }
    
    public string Rank { get; set; }

    [Range(0,1)]
    public float CommissionRatePercent { get; set; }
    
    public int EpxRequired { get; set; }

    public IList<Seller> Sellers { get; set; }
}