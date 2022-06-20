using PhuQuocVoucher.Api.Dtos.UserDto;
using static PhuQuocVoucher.Api.Dtos.IDto;

namespace PhuQuocVoucher.Api.Dtos;

public static class DtoConfig
{
    public static void Config()
    {
        Config<UserView>();
    }
}