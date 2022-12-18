using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ReviewDto;

public class ReviewView : BaseModel, IView<Review>, IDto
{
    public int Id { get; set; }

    public byte Rating { get; set; }

    public string Comment { get; set; }

    public int? VoucherId { get; set; }

    public string? VoucherName { get; set; }

    public int? CustomerId { get; set; }

    public string? CustomerName { get; set; }
    
    public void InitMapper()
    {
        TypeAdapterConfig<Review, ReviewView>.NewConfig()
            .Map(view => view.VoucherName, code => code.Voucher!.VoucherName)
            .Map(view => view.CustomerName, code => code.Customer!.CustomerName);
    }
    
}