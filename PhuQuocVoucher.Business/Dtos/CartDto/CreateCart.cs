using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CartDto;

public class CreateCart : CreateDto, ICreateRequest<Cart>
{
    public int CustomerId { get; set; }
}