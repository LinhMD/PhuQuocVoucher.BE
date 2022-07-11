using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using Provider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Business.Dtos.ProviderDto;

public class CreateProvider : CreateDto, ICreateRequest<Provider>
{
    [Required]
    public string ProviderName { get; set; }

    public string? Address { get; set; }

    [Required]
    public string TaxCode { get; set; }

    [Required]
    public int UserInfoId { get; set; }

    [Required]
    public int AssignedSellerId { get; set; }

    [Required]
    public int TypeId { get; set; }

}