using CrudApiTemplate.View;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.ServiceTypeDto;

public class ServiceTypeView : IView<ServiceType>, IDto
{
    public int Id { get; set; }

    public string Name { get; set; }
}