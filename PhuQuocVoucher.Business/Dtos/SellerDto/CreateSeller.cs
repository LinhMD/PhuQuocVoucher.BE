using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Business.Dtos.LoginDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.SellerDto;

public class CreateSeller : CreateDto, ICreateRequest<Seller>
{
    [Required] [MaxLength(255)] public string SellerName { get; set; }

    [Required] public SignUpRequest UserInfo { get; set; }

    [Required] public float CommissionRate { get; set; }
}