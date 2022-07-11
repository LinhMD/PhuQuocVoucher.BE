using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.SellerDto;

public class CreateSeller : CreateDto, ICreateRequest<Seller>
{
    [Required]
    [MaxLength(255)]
    public string SellerName { get; set; }

    [Required]
    public int UserInfoId { get; set; }

    [Required]
    public float CommissionRate { get; set; }
}