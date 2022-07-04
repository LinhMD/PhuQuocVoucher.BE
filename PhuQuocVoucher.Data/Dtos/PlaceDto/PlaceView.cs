using CrudApiTemplate.View;
using PhuQuocVoucher.Data.Dtos.BlogDto;
using PhuQuocVoucher.Data.Dtos.ServiceDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.PlaceDto;

public class PlaceView : IView<Place>, IDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string MapLocation { get; set; }

    public string Image { get; set; }

    public IEnumerable<BlogView> Blogs { get; set; }

    public IEnumerable<ServiceView> Services { get; set; }
}