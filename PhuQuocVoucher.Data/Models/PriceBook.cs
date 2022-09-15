namespace PhuQuocVoucher.Data.Models;

public class PriceBook
{
    public int Id { get; set; }
    
    public PriceLevel PriceLevel { get; set; }
    
    public int PriceLevelId { get; set; }
    
    public Product Product { get; set; }
    
    public int ProductId { get; set; }
    
    public double Price { get; set; }
}