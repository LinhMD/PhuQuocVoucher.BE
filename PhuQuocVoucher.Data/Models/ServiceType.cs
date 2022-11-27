using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class ServiceType : BaseModel, IOrderAble
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public ServiceType?  ParentType { get; set; }

    public int? ParentTypeId { get; set; }
    
    public double?  DefaultCommissionRate { get; set; }
    public void ConfigOrderBy()
    {
        Expression<Func<ServiceType, ModelStatus>> orderByStatus = type => type.Status;
        OrderByProvider<ServiceType>.OrderByDic.Add(nameof(Status),orderByStatus);
    }
}