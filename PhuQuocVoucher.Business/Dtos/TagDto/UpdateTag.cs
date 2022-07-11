using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.TagDto;

public class UpdateTag : UpdateDto, IUpdateRequest<Tag>
{
    public string? Name { get; set; }
}