using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ServiceTypeDto;

public class CreateServiceType : CreateDto, ICreateRequest<ServiceType>
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public int?  ParentTypeId { get; set; }
}