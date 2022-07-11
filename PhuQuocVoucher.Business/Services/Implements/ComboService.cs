using CrudApiTemplate.CustomException;
using CrudApiTemplate.Repository;
using CrudApiTemplate.Request;
using CrudApiTemplate.Services;
using CrudApiTemplate.Utilities;
using Microsoft.Extensions.Logging;
using PhuQuocVoucher.Business.Dtos.ComboDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Services.Implements;

public class ComboService : ServiceCrud<Combo>, IComboService
{
    private ILogger<ComboService> _logger;
    public ComboService(IUnitOfWork work, ILogger<ComboService> logger) : base(work.Get<Combo>(), work, logger)
    {
        _logger = logger;
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