namespace PhuQuocVoucher.Data.Models;

public class Product : BaseModel
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Summary { get; set; }

    public int Inventory { get; set; }
    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public ProductType Type { get; set; }

    public IEnumerable<PriceBook> Prices { get; set; }
    
    public IEnumerable<Tag> Tags { get; set; }

}