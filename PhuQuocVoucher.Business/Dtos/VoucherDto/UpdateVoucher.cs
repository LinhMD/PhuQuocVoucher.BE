using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class UpdateVoucher : UpdateDto, IUpdateRequest<VoucherCompaign>
{

    public string? VoucherName { get; set; }

    public double? Price { get; set; }

    public int? Inventory { get; set; }

    public int? LimitPerDay { get; set; }
    
    public double? DisplayPrice { get; set; }
    
    public string? BannerImg { get; set; }
    
    public string? Description { get; set; }

    public string? Summary { get; set; }

    
    public string? Content { get; set; }
    
    [Range(1, 20)]
    public int? SlotNumber { get; set; }


    public bool? IsRequireProfileInfo { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public ModelStatus? Status { get; set; }
}