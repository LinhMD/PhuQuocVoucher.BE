using CrudApiTemplate.Repository;
using CrudApiTemplate.Services;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class PlaceService : ServiceCrud<Place>, IPlaceService
{
    public PlaceService(IUnitOfWork work) : base(work.Get<Place>(), work)
    {
    }
}