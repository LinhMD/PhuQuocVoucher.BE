using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Business.Dtos.ProfileDto;
using PhuQuocVoucher.Business.Dtos.SellerDto;
using PhuQuocVoucher.Business.Dtos.UserDto;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.CustomerDto;

public class CustomerView : IView<Customer>, IDto
{
    public int Id { get; set; }

    public string CustomerName { get; set; }

    public UserView? UserInfo { get; set; }

    public int? UserInfoId { get; set; }
    
    public int? AssignSellerId { get; set; }
    
    public SellerSView AssignSeller { get; set; }
    
    public IList<ProfileView> Profiles { get; set; }

    public int CartId { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Customer, CustomerView>.NewConfig();
    }
}