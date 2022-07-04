using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.CustomerDto;

public class CreateCustomer : CreateDto, ICreateRequest<Customer>
{
    [Required]
    [MaxLength(255)]
    public string CustomerName { get; set; }

    [Required]
    public int UserInfoId { get; set; }

}