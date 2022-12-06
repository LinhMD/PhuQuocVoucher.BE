using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.VoucherDto;

public class FindVoucher : IFindRequest<Voucher>
{
    public int? Id { get; set; }

    [Contain] public string? VoucherName { get; set; }

    [BiggerThan(nameof(Voucher.SoldPrice))]
    public double? PriceLow { get; set; }

    [LessThan(nameof(Voucher.SoldPrice))]
    public double? PriceHigh { get; set; }

    public int? Inventory { get; set; }

    [BiggerThan(nameof(Voucher.SoldPrice))]
    public double? MinSellPrice { get; set; }

    [LessThan(nameof(Voucher.SoldPrice))]
    public double? MaxSellPrice { get; set; }

    [BiggerThan(nameof(BaseModel.UpdateAt))] public DateTime? UpdateAt_startTime { get; set; }

    [LessThan(nameof(BaseModel.UpdateAt))] public DateTime? UpdateAt_endTime { get; set; }

    [BiggerThan(nameof(BaseModel.CreateAt))] public DateTime? CreateAt_startTime { get; set; }

    [LessThan(nameof(BaseModel.CreateAt))] public DateTime? CrateAt_endTime { get; set; }

    [LessThan(nameof(Voucher.StartDate))]
    [BiggerThan(nameof(Voucher.EndDate))]
    public DateTime? Date { get; set; } = DateTime.Today;
    
    
    [BiggerThan(nameof(Voucher.StartDate))]
    [LessThan(nameof(Voucher.EndDate))]
    public DateTime? ExpiredDateHighBound { get; set; } 
    
    public int? ServiceId { get; set; }

    public int? ProviderId { get; set; }

    [Equal($"{nameof(Service)}.{nameof(Service.ServiceType)}.{nameof(ServiceType.Id)}")]
    public int? ServiceTypeId { get; set; }

    [In($"{nameof(Service)}.{nameof(Service.ServiceType)}.{nameof(ServiceType.Id)}")]
    public IList<int>? ServiceTypeIds { get; set; }

    [Contain(target: $"{nameof(Service)}.{nameof(Service.ServiceType)}.{nameof(ServiceType.Name)}")]
    public string? ServiceTypeName { get; set; }

    /// <summary>
    ///     Voucher.Product.Tags.Any(tag => tag.Name.Contains(TagName))
    /// </summary>
    [Any($"{nameof(Voucher.Tags)}", $"{nameof(VoucherTag.Tag)}.{nameof(Tag.Name)}", typeof(ContainAttribute))]
    public string? TagName { get; set; }

    public ModelStatus? Status { get; set; }

    public bool IsCombo => false;
}