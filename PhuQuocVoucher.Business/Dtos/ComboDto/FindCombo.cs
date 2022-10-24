using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ComboDto;

public class FindCombo : IFindRequest<Combo>
{
    public int? Id { get; set; }
    
    [Contain]
    public string? Name { get; set; }

   
    /*[LessThan("StartDate")]
    [BiggerThan("EndDate")]
    public DateTime? Date { get; } = DateTime.Now;*/


    public ModelStatus Status { get; set; }

    public double? Price { get; set; }
    
    [Equal]
    public int? ProductId { get; set; }

    [Any(nameof(Combo.Vouchers),nameof(Voucher.Id), typeof(EqualAttribute))]
    public int? ContainVoucherId { get; set; }
}