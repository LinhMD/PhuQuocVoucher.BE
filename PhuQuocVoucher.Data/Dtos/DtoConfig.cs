using PhuQuocVoucher.Data.Dtos.BlogDto;
using PhuQuocVoucher.Data.Dtos.CustomerDto;
using PhuQuocVoucher.Data.Dtos.ProviderDto;
using PhuQuocVoucher.Data.Dtos.ProviderTypeDto;
using PhuQuocVoucher.Data.Dtos.ServiceDto;
using PhuQuocVoucher.Data.Dtos.UserDto;
using static PhuQuocVoucher.Data.Dtos.IDto;

namespace PhuQuocVoucher.Data.Dtos;

public static class DtoConfig
{
    public static void Config()
    {
        Config<UserView>();
        Config<ProviderSView>();
        Config<CustomerSView>();
        Config<ServiceView>();

        Config<CreateUser>();
        Config<CreateProvider>();
        Config<CreateProviderType>();
        Config<CreateCustomer>();
        Config<CreateBlog>();
    }
}