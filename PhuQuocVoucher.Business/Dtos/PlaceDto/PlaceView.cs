using CrudApiTemplate.View;
using PhuQuocVoucher.Business.Dtos.BlogDto;
using PhuQuocVoucher.Business.Dtos.ServiceDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PlaceDto;

public class PlaceView : BaseModel,IView<Place>, IDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string MapLocation { get; set; }

    public string Image { get; set; }

    public IEnumerable<BlogView> Blogs { get; set; }

    public IEnumerable<ServiceView> Services { get; set; }
}