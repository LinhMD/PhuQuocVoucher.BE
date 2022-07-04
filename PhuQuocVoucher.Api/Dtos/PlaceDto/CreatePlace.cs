using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.PlaceDto;

public class CreatePlace : CreateDto, ICreateRequest<Place>
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string MapLocation { get; set; }

    [Required]
    public string Image { get; set; }
}