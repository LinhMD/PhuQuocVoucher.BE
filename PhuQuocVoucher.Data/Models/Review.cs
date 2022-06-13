namespace PhuQuocVoucher.Data.Models;

public class Review
{
    public int Id { get; set; }

    public byte Rating { get; set; }

    public string Comment { get; set; }

    public OrderItem OrderItem { get; set;}

    public int OrderItemId { get; set; }
}