using CrudApiTemplate.Request;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.LoginDto;

public class SignUpRequest : CreateDto, ICreateRequest<User>
{
    public string Email { get; set; }

    public Role Role { get; set; }

    public override void InitMapper()
    {
        TypeAdapterConfig<SignUpRequest, User>.NewConfig().Map(user => user.UserName, request => request.Email);
    }
}