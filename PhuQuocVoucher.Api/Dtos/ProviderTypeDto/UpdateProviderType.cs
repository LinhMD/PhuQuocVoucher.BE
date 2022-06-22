using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.ProviderTypeDto;

public class UpdateProviderType: UpdateDTO, IUpdateRequest<ProviderType>
{
    public string Name { get; set; }
}