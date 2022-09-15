using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Dtos.CustomerDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Core;

public interface ICustomerService : IServiceCrud<Customer>
{
    public Task<CustomerSView> CreateCustomerAsync(CreateCustomer createCustomer);
}