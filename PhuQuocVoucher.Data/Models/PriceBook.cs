using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class PriceBook : BaseModel, IOrderAble
{
    public int Id { get; set; }
    
    public PriceLevel PriceLevel { get; set; }
    

    [JsonIgnore]
    public Voucher Voucher { get; set; }

    public int VoucherId { get; set; }
    
    public double Price { get; set; }
    public void ConfigOrderBy()
    {
        Expression<Func<PriceBook, ModelStatus>> orderByStatus = price => price.Status;
        OrderByProvider<PriceBook>.OrderByDic.Add(nameof(Status),orderByStatus);
    }
}