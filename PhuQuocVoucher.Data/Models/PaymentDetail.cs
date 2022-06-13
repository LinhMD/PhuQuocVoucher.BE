namespace PhuQuocVoucher.Data.Models;

public class PaymentDetail
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string Content { get; set; }

    public int OrderId { get; set; }

    public Order Order { get; set; }
}