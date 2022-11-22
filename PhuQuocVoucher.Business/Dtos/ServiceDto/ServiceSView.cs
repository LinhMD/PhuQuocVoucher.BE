using CrudApiTemplate.View;
using Mapster;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PhuQuocVoucher.Business.Dtos.ProviderDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ServiceDto;

public class ServiceSView : IView<Service>, IDto

{
    public int? Id { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? TypeId { get; set; }
    
    public string? Type { get; set; }
    
    public string? LocationName { get; set; }

    public int? ServiceLocationId { get; set; }
    
    
    public string? ProviderName { get; set; }
    
    public int  ProviderId { get; set; }

    public ModelStatus Status { get; set; }
    
    public void InitMapper()
    {

        TypeAdapterConfig<Service, ServiceSView>.NewConfig()
            .Map(view => view.LocationName, service => service.ServiceLocation.Name)
            .Map(view => view.Type, service => service.ServiceType.Name)
            .Map(view => view.ProviderName, service => service.Provider.ProviderName);
    }
}