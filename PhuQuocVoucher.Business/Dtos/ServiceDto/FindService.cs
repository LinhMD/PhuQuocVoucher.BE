using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ServiceDto;

public class FindService : IFindRequest<Service>, IDto
{
    public int? Id { get; set; }

    [Contain] public string? Name { get; set; }

    [Contain] public string? Description { get; set; }

    [Contain($"{nameof(Service.ServiceType)}.{nameof(ServiceType.Id)}")] public int? TypeId { get; set; }

    [Contain($"{nameof(Service.ServiceType)}.{nameof(ServiceType.Name)}")] public string? TypeName { get; set; }

    [BiggerThan(nameof(BaseModel.UpdateAt))] public DateTime? UpdateAt_startTime { get; set; }

    [LessThan(nameof(BaseModel.UpdateAt))] public DateTime? UpdateAt_endTime { get; set; }

    [BiggerThan(nameof(BaseModel.CreateAt))] public DateTime? CreateAt_startTime { get; set; }

    [LessThan(nameof(BaseModel.CreateAt))] public DateTime? CrateAt_endTime { get; set; }

    [Equal($"{nameof(Service.ServiceLocation)}.{nameof(Place.Id)}")] public int? PlaceId { get; set; }

    [Contain($"{nameof(Service.ServiceLocation)}.{nameof(Place.Name)}")] public string? PlaceName { get; set; }

    [Equal($"{nameof(Service.Provider)}.{nameof(ServiceProvider.Id)}")] public int? ProviderId { get; set; }

    [Contain($"{nameof(Service.Provider)}.{nameof(ServiceProvider.ProviderName)}")] public string? ProviderName { get; set; }
}