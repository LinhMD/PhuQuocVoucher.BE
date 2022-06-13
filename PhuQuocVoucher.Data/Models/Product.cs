namespace PhuQuocVoucher.Data.Models;

public class Product
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Summary { get; set; }

    public string? BannerImg { get; set; }

    public string? Content { get; set; }

    public double Price { get; set; }

    public bool IsForKid { get; set; }

    public ProductType Type { get; set; }

    public Product? SiblingProduct { get; set; }

    public int? SiblingProductId { get; set; }

}