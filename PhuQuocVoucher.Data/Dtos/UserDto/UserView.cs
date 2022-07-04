using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.UserDto;

public class UserView : IView<User>, IDto
{
    public int? Id { get; set; }

    public string? Email { get; set; }

    public string? AvatarLink { get; set; }

    public string? UserName { get; set; }

    public Role? Role { get; set; }

    public string? PhoneNumber { get; set; }

    public ModelStatus? Status { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<User, UserView>.NewConfig();
    }
}