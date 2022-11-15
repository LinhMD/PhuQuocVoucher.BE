using Microsoft.Extensions.DependencyInjection;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Repositories;

public static class OrderByConfig
{
    public static void ConfigOrderBy(this IServiceCollection serviceCollection)
    {
        new Tag().ConfigOrderBy();
    }
}