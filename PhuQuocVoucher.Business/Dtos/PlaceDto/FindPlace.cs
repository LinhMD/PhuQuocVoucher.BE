using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PlaceDto;

public class FindPlace : IFindRequest<Place>
{
    [Equal]
    public int? Id { get; set; }

    [Contain(nameof(Place.Name))]
    public string? Name { get; set; }

    [Contain]
    public string? MapLocation { get; set; }

}