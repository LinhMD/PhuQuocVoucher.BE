using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using Microsoft.Extensions.Logging;
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
}