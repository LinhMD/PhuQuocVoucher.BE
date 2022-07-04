using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Dtos.UserDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.CustomerDto;

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