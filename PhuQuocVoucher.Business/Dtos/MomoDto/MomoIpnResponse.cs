namespace PhuQuocVoucher.Business.Dtos.MomoDto;

public class MomoIpnResponse
{
    public string partnerCode { get; set; }

    public string requestId { get; set; }

    public int orderId { get; set; }

    public double amount { get; set; }

    public long transId { get; set; }

    public int resultCode { get; set; }

    public string message { get; set; }

    public string requestType { get; set; }

    public long responseTime { get; set; }
}