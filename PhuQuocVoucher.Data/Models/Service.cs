using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using CrudApiTemplate.OrderBy;
using PhuQuocVoucher.Data.Repositories;
using PhuQuocVoucher.Data.Repositories.Core;

namespace PhuQuocVoucher.Data.Models;

public class Service : BaseModel, IOrderAble
{
    public int Id { get; set; }

    [Required]
    [MaxLength(2048)]
    public string Name { get; set; }

    public string Description { get; set; }

    public ServiceType ServiceType { get; set; }

    public int ServiceTypeId { get; set; }
    public Place ServiceLocation { get; set; }

    public double CommissionRate { get; set; }
    
    public int ServiceLocationId { get; set; }

    public ServiceProvider Provider { get; set; }

    public int ProviderId { get; set; }
    
    public void ConfigOrderBy()
    {
        SetUpOrderBy<Service>();
    }
}