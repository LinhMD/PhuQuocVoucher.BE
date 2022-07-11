using CrudApiTemplate.View;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.SellerDto;

public class SellerView : IView<Seller>, IDto
{
    public int? Id { get; set; }

    public string? SellerName { get; set; }

    public int? UserInfoId { get; set; }

    public UserView? UserInfo { get; set; }

    public float? CommissionRate { get; set; }
}