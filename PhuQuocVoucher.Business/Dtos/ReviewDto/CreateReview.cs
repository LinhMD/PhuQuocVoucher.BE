using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ReviewDto;

public class CreateReview : CreateDto, ICreateRequest<Review>
{
    [Range(1, 5)]
    public byte Rating { get; set; }

    public string Comment { get; set; }

    public int VoucherId { get; set; }

    public int CustomerId { get; set; }
}