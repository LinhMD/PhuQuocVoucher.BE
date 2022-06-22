using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.CustomerDto;

public class UpdateCustomer : UpdateDTO, IUpdateRequest<Customer>
{
    [Required]
    [MaxLength(255)]
    public string CustomerName { get; set; }


}