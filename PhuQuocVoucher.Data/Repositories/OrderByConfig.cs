using Microsoft.Extensions.DependencyInjection;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Repositories;

public static class OrderByConfig
{
    public static void ConfigOrderBy(this IServiceCollection serviceCollection)
    {
        new Blog().ConfigOrderBy();
        new Cart().ConfigOrderBy();
        new CartItem().ConfigOrderBy();
        new Customer().ConfigOrderBy();
        new Order().ConfigOrderBy();
        new OrderItem().ConfigOrderBy();
        new PaymentDetail().ConfigOrderBy();
        new Place().ConfigOrderBy();
        new PriceBook().ConfigOrderBy();
        new Profile().ConfigOrderBy();
        new Voucher().ConfigOrderBy();
        new Review().ConfigOrderBy();
        new Seller().ConfigOrderBy();
        new Service().ConfigOrderBy();
        new ServiceType().ConfigOrderBy();
        new Tag().ConfigOrderBy();
        new User().ConfigOrderBy();
        new VoucherCompaign().ConfigOrderBy();
        
    }
}