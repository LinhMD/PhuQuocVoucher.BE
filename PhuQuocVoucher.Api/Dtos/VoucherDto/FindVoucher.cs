using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.VoucherDto;

public class FindVoucher : IFindRequest<Voucher>
{
    public int Id { get; set; }

    [Contain]
    public string VoucherName { get; set; }

    public double? Price { get; set; }

    public int? Inventory { get; set; }

    public int? LimitPerDay { get; set; }

    public bool? IsRequireProfileInfo { get; set; }

    [BiggerThan("StartDate")]
    public DateTime? StartAfterDate { get; set; }

    [LessThan("EndDate")]
    public DateTime? EndBeforeDate { get; set; }

    public int? ProductId { get; set; }

    public int? ServiceId { get; set; }


}