using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.PaymentDetailDto;

public class PaymentDetailView : IView<PaymentDetail>, IDto
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string Content { get; set; }

    public int OrderId { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<PaymentDetail, PaymentDetailView>.NewConfig();
    }
}