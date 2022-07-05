using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using CrudApiTemplate.Utilities;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Dtos.ComboDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ComboService : ServiceCrud<Combo>, IComboService
{
    public ComboService(IUnitOfWork work) : base(work.Get<Combo>(), work)
    {
    }

    public async Task<Combo> CreateAsync(CreateCombo createCombo)
    {
        try
        {
            var productCreate = (createCombo.CreateProduct as ICreateRequest<Product>).CreateNew(UnitOfWork);
            productCreate.Type = ProductType.Combo;
            productCreate.Validate();
            var product = await UnitOfWork.Get<Product>().AddAsync(productCreate);

            var combo = (createCombo as ICreateRequest<Combo>).CreateNew(UnitOfWork);
            combo.Product = product;
            combo.Validate();
            return await UnitOfWork.Get<Combo>().AddAsync(combo);
        }
        catch (Exception e)
        {
            throw new DbQueryException(e.Message, DbError.Create);
        }
    }
}