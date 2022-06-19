namespace PhuQuocVoucher.Data.Models;

public class CartItem : BaseModel
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public DateTime CreateDate { get; set; }

    public Product Product { get; set; }

    public int ProductId { get; set; }

    public int CartId { get; set; }
}