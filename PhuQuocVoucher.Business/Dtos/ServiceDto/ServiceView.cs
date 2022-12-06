using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.ProviderDto;
using PhuQuocVoucher.Business.Dtos.ServiceTypeDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ServiceDto;

public class ServiceView : BaseModel, IView<Service>, IDto
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? ServiceTypeId { get; set; }
    public ServiceTypeView? ServiceType { get; set; }

    public double CommissionRate { get; set; }
    public string? LocationName { get; set; }

    public int? ServiceLocationId { get; set; }

    public SimpleProviderView? Provider { get; set; }

    public int ProviderId { get; set; }

    public ModelStatus Status { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Service, ServiceView>.NewConfig()
            .Map(view => view.LocationName, service => service.ServiceLocation.Name)
            .Map(view => view.ServiceType, service => service.ServiceType);
    }
}