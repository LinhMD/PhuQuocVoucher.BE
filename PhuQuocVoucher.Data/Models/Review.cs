using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class Review : BaseModel, IOrderAble

{
    public int Id { get; set; }

    public byte Rating { get; set; }

    public string Comment { get; set; }

    public int? VoucherId { get; set; }

    public int? CustomerId { get; set; }

    public Customer? Customer { get; set; }
    public void ConfigOrderBy()
    {
        
        SetUpOrderBy<Review>();
    }
}