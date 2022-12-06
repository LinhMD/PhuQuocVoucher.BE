using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PlaceDto;

public class PlaceSView : BaseModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string MapLocation { get; set; }

    public string Image { get; set; }
}