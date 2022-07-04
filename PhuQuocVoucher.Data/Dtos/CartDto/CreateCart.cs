using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.CartDto;

public class CreateCart : CreateDto, ICreateRequest<Cart>
{
    public int CustomerId { get; set; }
}