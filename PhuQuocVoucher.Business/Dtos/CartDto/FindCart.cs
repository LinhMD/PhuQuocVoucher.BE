using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CartDto;

public class FindCart : IFindRequest<Cart>, IDto
{

    public int? Id { get; set; }

    public int? CustomerId { get; set; }

    public ModelStatus Status { get; set; }
    
}