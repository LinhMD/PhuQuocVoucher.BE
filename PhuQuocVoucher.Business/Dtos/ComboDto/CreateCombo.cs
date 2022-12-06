using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ComboDto;

public class CreateCombo : CreateDto, ICreateRequest<Voucher>
{
    [Required] public string VoucherName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? BannerImg { get; set; }

    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string? Content { get; set; }
    
    public string?  SocialPost { get; set; }
    public IList<int> TagIds { get; set; }

    public IList<int> VoucherIds { get; set; }

    public long SoldPrice { get; set; }

    public long VoucherValue { get; set; }

    public bool IsCombo => true;

    public int Inventory { get; set; }
}