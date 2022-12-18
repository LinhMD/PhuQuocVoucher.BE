using PhuQuocVoucher.Business.Dtos.BlogDto;
using PhuQuocVoucher.Business.Dtos.CartDto;
using PhuQuocVoucher.Business.Dtos.CartItemDto;
using PhuQuocVoucher.Business.Dtos.ComboDto;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Business.Dtos.LoginDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Dtos.OrderItemDto;
using PhuQuocVoucher.Business.Dtos.PlaceDto;
using PhuQuocVoucher.Business.Dtos.ProviderDto;
using PhuQuocVoucher.Business.Dtos.QrCodeDto;
using PhuQuocVoucher.Business.Dtos.RankDto;
using PhuQuocVoucher.Business.Dtos.ReviewDto;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Business.Dtos.ServiceDto;
using PhuQuocVoucher.Business.Dtos.TagDto;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using static PhuQuocVoucher.Business.Dtos.IDto;

namespace PhuQuocVoucher.Business.Dtos;

public static class DtoConfig
{
    public static void ConfigMapper()
    {
        Config<BlogView>();
        Config<CartView>();
        Config<CartItemView>();
        Config<OrderView>();
        Config<UserView>();
        Config<ProviderView>();
        Config<PlaceView>();
        Config<CustomerSView>();
        Config<ServiceView>();
        Config<SellerView>();
        Config<SellerSView>();
        Config<SimpleProviderView>();
        Config<ComboView>();
        Config<ComboSView>();
        Config<QrCodeSView>();
        Config<OrderSView>();
        Config<OrderItemView>();
        Config<RankView>();
        Config<ReviewView>();
        
        Config<VoucherView>();
        Config<VoucherSView>();
        Config<CustomerSimpleView>();
        Config<CustomerView>();
        Config<TagView>();

        Config<CreateUser>();
        Config<CreateProvider>();
        Config<CreateCustomer>();
        Config<CreateBlog>();
        Config<SignUpRequest>();

        Config<CreateService>();
    }
}