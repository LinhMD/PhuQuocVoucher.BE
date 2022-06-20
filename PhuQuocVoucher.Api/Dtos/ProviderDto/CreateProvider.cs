using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using Mapster;
using PhuQuocVoucher.Data.Models;
using Provider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Api.Dtos.ProviderDto;

public class CreateProvider : CreateDTO, ICreateRequest<Provider>
{
    [Required]
    public string ProviderName { get; set; }

    public string? Address { get; set; }

    [Required]
    public string TaxCode { get; set; }

    public int UserInfoId { get; set; }

    public int AssignedSellerId { get; set; }

    public int TypeId { get; set; }

    public override void InitMapper()
    {
        TypeAdapterConfig<CreateProvider, Provider>.NewConfig();
    }
}