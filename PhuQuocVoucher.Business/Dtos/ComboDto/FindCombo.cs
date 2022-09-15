using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ComboDto;

public class FindCombo : IFindRequest<Combo>
{
    public int? Id { get; set; }
    
    [Contain]
    public string? Name { get; set; }

    [BiggerThan("StartDate")]
    public DateTime? StartAfterDate { get; set; }

    [LessThan("EndDate")]
    public DateTime? EndBeforeDate { get; set; }

    public int? Status { get; set; }

    public double? Price { get; set; }
    
    [Equal]
    public int? ProductId { get; set; }

    [Any(nameof(Combo.Vouchers),nameof(Voucher.Id), typeof(EqualAttribute))]
    public int? ContainVoucherId { get; set; }
}