using CrudApiTemplate.Repository;
using Microsoft.EntityFrameworkCore;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Repositories.Implements;

public class PlaceRepository : Repository<Place>, IPlaceRepository
{
    public PlaceRepository(DbContext context) : base(context)
    {
    }
}