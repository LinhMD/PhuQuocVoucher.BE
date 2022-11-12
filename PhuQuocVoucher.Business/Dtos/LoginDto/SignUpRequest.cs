using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.LoginDto;

public class SignUpRequest : CreateDto, ICreateRequest<User>
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    

    [Required]
    public Role Role { get; set; }

    public override void InitMapper()
    {
        TypeAdapterConfig<SignUpRequest, User>.NewConfig().Map(user => user.UserName, request => request.Email);
    }
}