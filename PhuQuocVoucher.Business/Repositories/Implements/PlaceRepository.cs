using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Business.Repositories.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Repositories.Implements;

public class PlaceRepository : Repository<Place>, IPlaceRepository
{
    public PlaceRepository(DbContext context) : base(context)
    {
    }
}