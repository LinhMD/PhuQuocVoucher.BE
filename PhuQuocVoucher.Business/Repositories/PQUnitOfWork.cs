using CrudApiTemplate.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Business.Repositories;

public class PQUnitOfWork : UnitOfWork, IUnitOfWork
{
    public PQUnitOfWork(DbContext dataContext) : base(dataContext)
    {

    }
}