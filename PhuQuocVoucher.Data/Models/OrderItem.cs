namespace PhuQuocVoucher.Data.Models;

public class OrderItem  : BaseModel
{
    public int Id { get; set;}

    public int OrderId { get; set; }

    public Order Order { get; set; }

    public Product OrderProduct { get; set; }

    public int OrderProductId { get; set; }

    public Profile? Profile { get; set; }

    public int? ProfileId { get; set; }
    public Review? Review { get; set; }

    public DateTime UseDate { get; set; }


}