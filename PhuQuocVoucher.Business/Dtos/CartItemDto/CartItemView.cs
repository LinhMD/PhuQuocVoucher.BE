using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CartItemDto;

public class CartItemView : BaseModel, IView<CartItem>, IDto
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public VoucherSView Voucher { get; set; }

    public int VoucherId { get; set; }

    public bool IsCombo { get; set; }


    public void InitMapper()
    {
        TypeAdapterConfig<CartItem, CartItemView>.NewConfig();
    }
}