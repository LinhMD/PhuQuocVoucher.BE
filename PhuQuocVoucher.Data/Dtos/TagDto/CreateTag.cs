using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.TagDto;

public class CreateTag : CreateDto, ICreateRequest<Tag>
{
    [Required]
    public string Name { get; set; }
}