namespace PhuQuocVoucher.Business.Dtos.MomoDto;

public class MomoIPNRequest
{
    public string? Amount { get; set; }
    
    public string? ExtraData { get; set; }
    
    public string? Message { get; set; }
    
    public int OrderId { get; set; }
    
    public string? OrderInfo { get; set; }
    
    public string? OrderType { get; set; }
    
    public string? PartnerCode { get; set; }
    
    public string? PayType { get; set; }
    
    public string? RequestId { get; set; }
    
    public string? ResponseTime { get; set; }
    
    public string? ResultCode { get; set; }
    
    public string? Signature { get; set; }
    
    public string? TransId { get; set; }
    
}