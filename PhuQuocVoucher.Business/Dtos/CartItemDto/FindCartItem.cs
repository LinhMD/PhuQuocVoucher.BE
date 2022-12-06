using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CartItemDto;

public class FindCartItem : IFindRequest<CartItem>, IDto
{
    public int? Id { get; set; }

    public int? Quantity { get; set; }

    public int? voucherId { get; set; }

    public int? CartId { get; set; }

    [BiggerThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_startTime { get; set; }

    [LessThan(nameof(BaseModel.UpdateAt))] public DateTime? UpdateAt_endTime { get; set; }

    [BiggerThan(nameof(BaseModel.CreateAt))]
    public DateTime? CreateAt_startTime { get; set; }

    [LessThan(nameof(BaseModel.CreateAt))] public DateTime? CrateAt_endTime { get; set; }
}