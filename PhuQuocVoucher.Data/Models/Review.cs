namespace PhuQuocVoucher.Data.Models;

public class Review : BaseModel
{
    public int Id { get; set; }

    public byte Rating { get; set; }

    public string Comment { get; set; }

    public OrderItem OrderItem { get; set;}

    public int OrderItemId { get; set; }
}