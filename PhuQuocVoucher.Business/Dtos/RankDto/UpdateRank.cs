using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.RankDto;

public class UpdateRank : UpdateDto, IUpdateRequest<SellerRank> 
{
    public string? Logo { get; set; }
    
    public string? Rank { get; set; }

    [Range(0,1)]
    public float? CommissionRatePercent { get; set; }
    
    public int? EpxRequired { get; set; }
}