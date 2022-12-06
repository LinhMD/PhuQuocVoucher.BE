using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Attributes.Search;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.UserDto;

public class FindUser : IFindRequest<User>, IDto
{
    [In("Id")] public IList<int>? Ids { get; set; }

    [In("Email")] public IList<string>? Emails { get; set; }

    [In("PhoneNumber")] public IList<string>? PhoneNumbers { get; set; }

    [BiggerThan(nameof(BaseModel.UpdateAt))]
    public DateTime? UpdateAt_startTime { get; set; }

    [LessThan(nameof(BaseModel.UpdateAt))] public DateTime? UpdateAt_endTime { get; set; }

    [BiggerThan(nameof(BaseModel.CreateAt))]
    public DateTime? CreateAt_startTime { get; set; }

    [LessThan(nameof(BaseModel.CreateAt))] public DateTime? CrateAt_endTime { get; set; }

    [Equal] public ModelStatus? Status { get; set; }

    [Equal] public Role? Role { get; set; }

    [MaxLength(255)] [Equal] public string? UserName { get; set; }
}