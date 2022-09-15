using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Mapster;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class CustomerService : ServiceCrud<Customer>, ICustomerService
{
    private ILogger<CustomerService> _logger;
    public CustomerService(IUnitOfWork work, ILogger<CustomerService> logger) : base(work.Get<Customer>(), work,logger)
    {
        _logger = logger;
    }

    public async Task<CustomerSView> CreateCustomerAsync(CreateCustomer createCustomer)
    {
        var user = createCustomer.UserInfo.Adapt<User>();
        user.Role = Role.Seller;
        await UnitOfWork.Get<User>().AddAsync(user);
        var customer = new Customer()
        {
            CustomerName = createCustomer.CustomerName,
            CreateAt = createCustomer.CreateAt,
            Status = createCustomer.Status,
            UserInfoId = user.Id,
            UserInfo = user
        };

        await Repository.AddAsync(customer);

        var cart = new Cart() {CustomerId = customer.Id};

        await UnitOfWork.Get<Cart>().AddAsync(cart);

        customer.CartId = cart.Id;
        customer.Cart = cart;
        await Repository.CommitAsync();
        return customer.Adapt<CustomerSView>();
    }
}