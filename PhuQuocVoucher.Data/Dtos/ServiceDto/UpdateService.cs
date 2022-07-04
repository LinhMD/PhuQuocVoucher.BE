using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.ServiceDto;

public class UpdateService : UpdateDto, IUpdateRequest<Service>
{
    public string Name { get; set; }

    public string Description { get; set; }

}