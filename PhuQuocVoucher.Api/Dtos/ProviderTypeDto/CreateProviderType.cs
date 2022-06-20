using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.ProviderTypeDto;

public class CreateProviderType : CreateDTO, ICreateRequest<ProviderType>
{
    public string Name { get; set; }
}