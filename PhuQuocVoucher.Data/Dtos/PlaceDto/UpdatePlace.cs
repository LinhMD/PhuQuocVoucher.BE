using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.PlaceDto;

public class UpdatePlace : UpdateDto, IUpdateRequest<Place>
{
    public string? Name { get; set; }

    public string? MapLocation { get; set; }

    public string? Image { get; set; }
}