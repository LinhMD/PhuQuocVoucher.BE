using System.ComponentModel.DataAnnotations;

namespace PhuQuocVoucher.Business.Dtos.MomoDto;

public class PaymentRequest
{
    public string PartnerCode { get; set; }
    
    public string? PartnerName { get; set; }

    public string? storeId { get; set; }
    
    [MaxLength(50)]
    public string requestId { get; set; }

    public long Amount { get; set; }

    public string orderId { get; set; }

    public string orderInfo { get; set; }

    public string? orderGroupId { get; set; }

    public string redirectUrl { get; set; }

    public string  ipnUrl { get; set; }

    public string requestType { get; set; } = "captureWallet";

    public string extraData { get; set; } = "";

    public object userInfo { get; set; }
    
    public bool autoCapture { get; set; }
    
    public string lang { get; set; }

    public string signature { get; set; }
}