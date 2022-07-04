using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Dtos.ProviderDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.ServiceDto;

public class ServiceView : IView<Service>, IDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Type { get; set; }
    public string LocationName { get; set; }

    public ProviderSView Provider { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Service, ServiceView>.NewConfig()
            .Map(view => view.LocationName, service => service.ServiceLocation.Name)
            .Map(view => view.Type, service => service.Type.Name);
    }
}