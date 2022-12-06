namespace PhuQuocVoucher.Business.Dtos.MomoDto;

public class MomoIPNRequest
{
    public long Amount { get; set; }

    public string ExtraData { get; set; }

    public string Message { get; set; }

    public string OrderId { get; set; }

    public string OrderInfo { get; set; }

    public string OrderType { get; set; }

    public string PartnerCode { get; set; }

    public string PayType { get; set; }

    public string RequestId { get; set; }

    public long ResponseTime { get; set; }

    public int ResultCode { get; set; }

    public string Signature { get; set; }

    public long TransId { get; set; }
}