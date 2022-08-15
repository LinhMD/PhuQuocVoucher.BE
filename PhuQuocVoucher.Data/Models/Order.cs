namespace PhuQuocVoucher.Data.Models;

public class Order : BaseModel
{
    public int Id { get; set; }

    public double TotalPrice { get; set; }

    public DateTime CompleteDate { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Processing;
    public Customer? Customer { get; set; }

    public int? CustomerId { get; set; }
    public Seller? Seller { get; set; }

    public int? SellerId { get; set; }

    public PaymentDetail? PaymentDetail { get; set; }

    public IEnumerable<OrderItem> OrderItems { get; set; }

}