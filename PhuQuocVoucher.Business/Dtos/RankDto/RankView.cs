using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.RankDto;

public class RankView : BaseModel, IView<SellerRank>, IDto
{
    public int Id { get; set; }

    public string Logo { get; set; }
    
    public string Rank { get; set; }

    public float CommissionRatePercent { get; set; }
    
    public int EpxRequired { get; set; }

    public int NumberOfSeller { get; set; }
    
    public void InitMapper()
    {
        TypeAdapterConfig<SellerRank, RankView>.NewConfig()
            .Map(view => view.NumberOfSeller, code => code.Sellers.Count);
    }
}