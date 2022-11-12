using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;
using Provider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Business.Dtos.ProviderDto;

public class UpdateProvider :  UpdateDto, IUpdateRequest<Provider>
{
    public string? ProviderName { get; set; }

    public string? Address { get; set; }

    public string? TaxCode { get; set; }

    public int? UserInfoId { get; set; }

    public int? AssignedSellerId { get; set; }

    public int? TypeId { get; set; }
    
    public ModelStatus? Status { get; set; }
}