using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.SellerDto;

public class UpdateSeller : UpdateDto, IUpdateRequest<Seller>
{
    [MaxLength(255)]
    public string? SellerName { get; set; }

    public float? CommissionRate { get; set; }
    
    public ModelStatus? Status { get; set; }
}