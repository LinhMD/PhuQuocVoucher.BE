using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.ProviderDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProviderTypeDto;

public class ProviderTypeSView : IView<ProviderType>, IDto
{

    public int Id { get; set; }

    public string Name { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<ProviderType, ProviderTypeSView>.NewConfig();
    }
}