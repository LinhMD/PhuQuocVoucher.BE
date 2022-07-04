using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.CartDto;

public class FindCart : IFindRequest<Cart>, IDto
{

    public int? Id { get; set; }

    public int? CustomerId { get; set; }

}