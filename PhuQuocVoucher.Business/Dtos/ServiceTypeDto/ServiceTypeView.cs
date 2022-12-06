using CrudApiTemplate.View;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ServiceTypeDto;

public class ServiceTypeView : BaseModel,  IView<ServiceType>, IDto
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public double?  DefaultCommissionRate { get; set; }
}