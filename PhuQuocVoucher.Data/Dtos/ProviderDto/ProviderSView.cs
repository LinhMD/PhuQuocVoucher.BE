using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Dtos.UserDto;
using PhuQuocVoucher.Data.Models;
using Provider = PhuQuocVoucher.Data.Models.ServiceProvider;

namespace PhuQuocVoucher.Data.Dtos.ProviderDto;

public class ProviderSView : IView<Provider>, IDto
{
    public int Id { get; set; }

    [Required]
    public string? ProviderName { get; set; }

    public string? Address { get; set; }

    public string? TaxCode { get; set; }

    public UserView? UserInfo { get; set; }

    public Seller? AssignedSeller { get; set; }

    public int AssignedSellerId { get; set; }

    public ProviderType? Type { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Provider, ProviderSView>.NewConfig();
    }
}