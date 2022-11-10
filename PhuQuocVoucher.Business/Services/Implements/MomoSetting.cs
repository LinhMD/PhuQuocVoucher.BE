using Microsoft.Extensions.Configuration;

namespace PhuQuocVoucher.Business.Services.Implements;

public class MomoSetting
{

    public MomoSetting(IConfiguration config)
    {
        WebRedirect = config["MomoSetting:WebRedirect"];
        MobileRedirect = config["MomoSetting:PartnerCode"];
        PartnerCode = config["MomoSetting:PartnerCode"];
        AccessKey = config["MomoSetting:AccessKey"];
        SecretKey = config["MomoSetting:SecretKey"];
        EndPoint = config["MomoSetting:EndPoint"];
        IpnCallback = config["MomoSetting:IpnCallback"];
    }
    
    public string PartnerCode { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string EndPoint { get; set; }
    public string IpnCallback { get; set; }

    public string MobileRedirect { get; set; }

    public string WebRedirect { get; set; }
}