namespace PhuQuocVoucher.Data.Models;

public class Payment
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string Content { get; set; }
}