using Microsoft.Extensions.DependencyInjection;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Business.Services.Implements;

namespace PhuQuocVoucher.Business.Services;

public static class ServicesInitDi
{
    public static void InitServices(
        this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<ICustomerService, CustomerService>();
    }
}