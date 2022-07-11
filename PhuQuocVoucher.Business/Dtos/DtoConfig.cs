using PhuQuocVoucher.Business.Dtos.BlogDto;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Business.Dtos.ProviderDto;
using PhuQuocVoucher.Business.Dtos.ProviderTypeDto;
using PhuQuocVoucher.Business.Dtos.ServiceDto;
using PhuQuocVoucher.Business.Dtos.UserDto;
using static PhuQuocVoucher.Business.Dtos.IDto;

namespace PhuQuocVoucher.Business.Dtos;

public static class DtoConfig
{
    public static void ConfigMapper()
    {
        Config<UserView>();
        Config<ProviderView>();
        Config<CustomerSView>();
        Config<ServiceView>();
        Config<SimpleProviderView>();

        Config<CreateUser>();
        Config<CreateProvider>();
        Config<CreateProviderType>();
        Config<CreateCustomer>();
        Config<CreateBlog>();
    }
}