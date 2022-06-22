using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class CustomerService : ServiceCrud<Customer>, ICustomerService
{
    public CustomerService( IUnitOfWork work) : base(work.Get<Customer>(), work)
    {
    }
}