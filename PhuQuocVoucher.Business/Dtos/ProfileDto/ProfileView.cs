using CrudApiTemplate.View;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProfileDto;

public class ProfileView : IView<Profile>, IDto
{
    public int Id { get; set; }

    public int Sex { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Name { get; set; }

    public string CivilIdentify { get; set; }

    public int CustomerId { get; set; }

}