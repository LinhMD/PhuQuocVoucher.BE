using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CustomerDto;

public class CustomerSimpleView : IView<Customer>, IDto
{
    public int Id { get; set; }

    public string CustomerName { get; set; }

    public int? UserInfoId { get; set; }

    public int CartId { get; set; }


    public void InitMapper()
    {
        TypeAdapterConfig<Customer, CustomerSimpleView>.NewConfig();
    }
}