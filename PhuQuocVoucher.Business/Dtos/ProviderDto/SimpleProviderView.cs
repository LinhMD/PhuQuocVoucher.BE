using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProviderDto;

public class SimpleProviderView : BaseModel,IView<ServiceProvider>, IDto
{
    public int Id { get; set; }

    public string? ProviderName { get; set; }

    public string? Address { get; set; }

    public string? TaxCode { get; set; }


    public void InitMapper()
    {
        TypeAdapterConfig<ServiceProvider, SimpleProviderView>.NewConfig();
    }
}