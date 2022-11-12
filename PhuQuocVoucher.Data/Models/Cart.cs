using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(CustomerId), IsUnique = true)]
public class Cart : BaseModel
{
    public int Id { get; set; }

    public Customer Customer { get; set; }

    public int CustomerId { get; set; }

    public IList<CartItem> CartItems { get; set; }
}