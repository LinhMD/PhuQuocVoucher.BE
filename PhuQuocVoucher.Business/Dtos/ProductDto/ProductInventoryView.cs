using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PhuQuocVoucher.Business.Dtos.ProductDto;

public class ProductInventoryView
{
    public int Id { get; set; }

    public int Inventory { get; set; }
}