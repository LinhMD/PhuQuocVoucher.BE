
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.RankDto;

public class FindRank : IFindRequest<SellerRank>
{

    public int? Id { get; set; }
    
    public string? Logo { get; set; }
    
    public string? Rank { get; set; }

    public int? EpxRequired { get; set; }
}