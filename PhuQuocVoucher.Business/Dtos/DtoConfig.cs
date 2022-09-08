using PhuQuocVoucher.Business.Dtos.BlogDto;
using PhuQuocVoucher.Business.Dtos.ComboDto;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Business.Dtos.OrderDto;
using PhuQuocVoucher.Business.Dtos.PlaceDto;
using PhuQuocVoucher.Business.Dtos.ProductDto;
using PhuQuocVoucher.Business.Dtos.ProviderDto;
using PhuQuocVoucher.Business.Dtos.ProviderTypeDto;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Business.Dtos.ServiceDto;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Business.Dtos.VoucherDto;
using PhuQuocVoucher.Data.Models;
using static PhuQuocVoucher.Business.Dtos.IDto;

namespace PhuQuocVoucher.Business.Dtos;

public static class DtoConfig
{
    public static void ConfigMapper()
    {
        Config<BlogView>();
        Config<ComboView>();
        Config<OrderView>();
        Config<UserView>();
        Config<ProviderView>();
        Config<PlaceView>();
        Config<CustomerSView>();
        Config<ServiceView>();
        Config<SellerView>();
        Config<SellerSView>();
        Config<SimpleProviderView>();
        Config<ProviderTypeSView>();
        Config<ProductView>();
        Config<VoucherView>();
        Config<VoucherSView>();

        Config<CreateUser>();
        Config<CreateProvider>();
        Config<CreateProviderType>();
        Config<CreateCustomer>();
        Config<CreateBlog>();
    }
}