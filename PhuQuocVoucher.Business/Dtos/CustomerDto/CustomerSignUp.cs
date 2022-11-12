using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CustomerDto;

public class CustomerSignUp : CreateDto, ICreateRequest<Customer>
{
    
    [Required]
    [MaxLength(255)]
    public string CustomerName { get; set; }
    

    [Required]
    public UserSignUp UserInfo { get; set; }
}