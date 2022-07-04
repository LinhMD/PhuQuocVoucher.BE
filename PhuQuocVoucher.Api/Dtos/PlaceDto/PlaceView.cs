using CrudApiTemplate.View;
using PhuQuocVoucher.Api.Dtos.BlogDto;
using PhuQuocVoucher.Api.Dtos.ServiceDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.PlaceDto;

public class PlaceView : IView<Place>, IDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string MapLocation { get; set; }

    public string Image { get; set; }

    public IEnumerable<BlogView> Blogs { get; set; }

    public IEnumerable<ServiceView> Services { get; set; }
}