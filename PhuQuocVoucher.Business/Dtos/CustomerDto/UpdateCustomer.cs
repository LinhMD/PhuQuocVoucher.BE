using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CustomerDto;

public class UpdateCustomer : UpdateDto, IUpdateRequest<Customer>
{
    [Required]
    [MaxLength(255)]
    public string CustomerName { get; set; }

}