using System.Linq.Expressions;
using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class FindVoucher : IFindRequest<Voucher>
{
    public int? Id { get; set; }

    [Contain]
    public string? VoucherName { get; set; }

    [BiggerThan("Price")]
    public double? PriceLow { get; set; }
    
    [LessThan("Price")]
    public double? PriceHigh { get; set; }

    public int? Inventory { get; set; }

    public int? LimitPerDay { get; set; }

    public bool? IsRequireProfileInfo { get; set; }

    /*[LessThan("StartDate")]
    [BiggerThan("EndDate")]
    public DateTime? Date { get; set; } = DateTime.Now;*/

    public int? VoucherId { get; set; }

    public int? ServiceId { get; set; }
    
    [Equal($"{nameof(Service)}.{nameof(Service.TypeId)}")]
    public int? ServiceTypeId{ get; set; }
    
    [Contain(target:$"{nameof(Service)}.{nameof(Service.Type)}.{nameof(ServiceType.Name)}")]
    public string? ServiceTypeName { get; set; }

    /// <summary>
    /// Voucher.Product.Tags.Any(tag => tag.Name.Contains(TagName))
    /// </summary>
    [Any(target:$"{nameof(Voucher.Tags)}", property:nameof(Tag.Name), typeof(ContainAttribute))]
    public string? TagName { get; set; }
    
    public ModelStatus Status { get; set; }

}