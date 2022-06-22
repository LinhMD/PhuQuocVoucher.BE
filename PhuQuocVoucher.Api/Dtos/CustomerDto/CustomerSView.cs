using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Api.Dtos.UserDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.CustomerDto;

public class CustomerSView : IView<Customer>, IDto
{
    public int Id { get; set; }

    public string CustomerName { get; set; }

    public UserView? UserInfo { get; set; }

    public int? UserInfoId { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Customer, CustomerSView>.NewConfig();
    }
}