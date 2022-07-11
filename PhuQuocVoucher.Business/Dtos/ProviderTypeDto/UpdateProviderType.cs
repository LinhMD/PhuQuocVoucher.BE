using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ProviderTypeDto;

public class UpdateProviderType: UpdateDto, IUpdateRequest<ProviderType>
{
    public string Name { get; set; }

}