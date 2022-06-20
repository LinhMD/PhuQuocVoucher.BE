using CrudApiTemplate.View;
using PhuQuocVoucher.Api.Dtos.UserDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos.CustomerDto;

public class CustomerSView : IView<Customer>
{
    public int Id { get; set; }

    public string CustomerName { get; set; }

    public UserView? UserInfo { get; set; }

    public int? UserInfoId { get; set; }
}