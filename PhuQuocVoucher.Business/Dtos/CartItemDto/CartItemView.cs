using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CartItemDto;

public class CartItemView : IView<CartItem>, IDto
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public VoucherSView Voucher { get; set; }

    public int VoucherId { get; set; }
    public double Price { get; set; }
    
    public int PriceId { get; set; }

    public int? ProfileId { get; set; }
    
    public DateTime? UseDate { get; set; }
    
    
    public void InitMapper()
    {
        TypeAdapterConfig<CartItem, CartItemView>.NewConfig()
            .Map(view => view.Price, item => item.Price!.Price);
    }
}