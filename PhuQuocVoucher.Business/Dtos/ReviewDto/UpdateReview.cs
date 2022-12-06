using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ReviewDto;

public class UpdateReview : UpdateDto, IUpdateRequest<Review>
{
    public byte? Rating { get; set; }

    public string? Comment { get; set; }
}