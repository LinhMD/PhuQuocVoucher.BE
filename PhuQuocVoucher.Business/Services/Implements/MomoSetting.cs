using Microsoft.Extensions.Configuration;

namespace PhuQuocVoucher.Business.Services.Implements;

public class MomoSetting
{

    public MomoSetting(IConfiguration config)
    {
        PartnerCode = config["MomoSetting:PartnerCode"];
        AccessKey = config["MomoSetting:AccessKey"];
        SecretKey = config["MomoSetting:SecretKey"];
        EndPoint = config["MomoSetting:EndPoint"];
    }
    
    public string? PartnerCode { get; set; }
    public string? AccessKey { get; set; }
    public string? SecretKey { get; set; }
    public string? EndPoint { get; set; }
}