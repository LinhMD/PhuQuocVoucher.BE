using CrudApiTemplate.Repositories;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Business.Repositories.Implements;
using PhuQuocVoucher.Data;

namespace PhuQuocVoucher.Business.Repositories;

public class PqUnitOfWork : UnitOfWork
{
    private readonly PhuQuocDataContext _dataContext;

    public PqUnitOfWork(PhuQuocDataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
        Users = new UserRepository(dataContext);
        this.Add(Users);
    }

    public IUserRepository Users { get; }

}