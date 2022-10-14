using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Business.Dtos.UserDto;

namespace PhuQuocVoucher.Business.Dtos.ServiceDto;

public class SellerSignUp : CreateDto, ICreateRequest<SellerSignUp>
{
    [Required]
    [MaxLength(255)]
    public string SellerName { get; set; }

    [Required]
    public UserSignUp UserInfo { get; set; }
}