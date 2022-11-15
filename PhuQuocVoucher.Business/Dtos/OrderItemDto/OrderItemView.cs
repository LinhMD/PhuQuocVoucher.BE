using CrudApiTemplate.View;
using Mapster;
using Newtonsoft.Json;
using PhuQuocVoucher.Business.Dtos.PriceBookDto;
using PhuQuocVoucher.Business.Dtos.ProfileDto;
using PhuQuocVoucher.Business.Dtos.QrCodeDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.OrderItemDto;

public class OrderItemView : IView<OrderItem>, IDto
{
    public int Id { get; set;}

    public int OrderId { get; set; }

    public int VoucherId { get; set; }
    public string Image { get; set; }
    public string VoucherName { get; set; }

    public int? ProfileId { get; set; }
    
    public ProfileView? Profile { get; set; }
    
    public DateTime? UseDate { get; set; }
    
    public double? SoldPrice { get; set; }
    
    public PriceBookSView Price { get; set; }
    
    public int PriceId { get; set; }
    
    public QrCodeView QrCode { get; set; }


    public void InitMapper()
    {
        TypeAdapterConfig<OrderItem, OrderItemView>.NewConfig()
            .Map(view => view.Image, item=> item.Voucher.BannerImg)
            .Map(view => view.VoucherName, item=> item.Voucher.VoucherName);
    }
}