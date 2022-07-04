using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.TagDto;

public class UpdateTag : UpdateDto, IUpdateRequest<Tag>
{
    public string? Name { get; set; }
}