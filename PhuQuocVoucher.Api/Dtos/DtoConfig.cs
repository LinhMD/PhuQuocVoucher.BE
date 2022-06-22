﻿using PhuQuocVoucher.Api.Dtos.CustomerDto;
using PhuQuocVoucher.Api.Dtos.ProviderDto;
using PhuQuocVoucher.Api.Dtos.ProviderTypeDto;
using PhuQuocVoucher.Api.Dtos.UserDto;
using static PhuQuocVoucher.Api.Dtos.IDto;

namespace PhuQuocVoucher.Api.Dtos;

public static class DtoConfig
{
    public static void Config()
    {
        Config<UserView>();
        Config<ProviderSView>();
        Config<CustomerSView>();


        Config<CreateUser>();
        Config<CreateProvider>();
        Config<CreateProviderType>();
        Config<CreateCustomer>();

    }
}