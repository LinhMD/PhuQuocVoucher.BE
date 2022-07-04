using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.ProviderTypeDto;

public class CreateProviderType : CreateDto, ICreateRequest<ProviderType>
{
    [Required]
    public string Name { get; set; }
}