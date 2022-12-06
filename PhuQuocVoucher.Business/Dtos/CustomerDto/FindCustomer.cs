using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CustomerDto;

public class FindCustomer : IFindRequest<Customer>
{
    public int? Id { get; set; }

    [In("Id")] public IList<int>? Ids { get; set; }

    [In("UserInfo.Id")] public IList<int>? UserIds { get; set; }

    [In("UserInfo.Email")] public IList<string>? Emails { get; set; }

    [In("UserInfo.PhoneNumber")] public IList<string>? PhoneNumbers { get; set; }

    [Contain] public string? CustomerName { get; set; }

    [Equal("UserInfoId")] public int? UserId { get; set; }

    [Contain(target: "UserInfo.UserName")] public string? UserName { get; set; }

    [Contain(target: "UserInfo.Email")] public string? Email { get; set; }

    [Contain(target: "UserInfo.PhoneNumber")] public string? PhoneNumber { get; set; }

    [Equal(nameof(Customer.AssignSellerId))] public int? SellerId { get; set; }

    [BiggerThan(nameof(BaseModel.UpdateAt))] public DateTime? UpdateAt_startTime { get; set; }

    [LessThan(nameof(BaseModel.UpdateAt))] public DateTime? UpdateAt_endTime { get; set; }

    [BiggerThan(nameof(BaseModel.CreateAt))] public DateTime? CreateAt_startTime { get; set; }

    [LessThan(nameof(BaseModel.CreateAt))] public DateTime? CrateAt_endTime { get; set; }
}