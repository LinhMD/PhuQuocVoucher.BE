using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.ProviderTypeDto;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProviderDto;

public class ProviderView : IView<ServiceProvider>, IDto
{
    public int Id { get; set; }

    [Required]
    public string? ProviderName { get; set; }

    public string? Address { get; set; }

    public string? TaxCode { get; set; }

    public UserView? UserInfo { get; set; }

    public SellerView? AssignedSeller { get; set; }

    public int AssignedSellerId { get; set; }

    public ProviderTypeSView? Type { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<ServiceProvider, ProviderView>.NewConfig();
    }
}