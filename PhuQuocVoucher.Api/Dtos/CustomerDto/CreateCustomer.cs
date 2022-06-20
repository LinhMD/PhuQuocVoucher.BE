using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.CustomerDto;

public class CreateCustomer : CreateDTO, ICreateRequest<Customer>
{
    [Required]
    [MaxLength(255)]
    public string CustomerName { get; set; }

    [Required]
    public int UserInfoId { get; set; }

    public override void InitMapper()
    {
        TypeAdapterConfig<CreateCustomer, Customer>.NewConfig();
    }
}