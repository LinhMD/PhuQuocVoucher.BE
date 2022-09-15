namespace PhuQuocVoucher.Data.Models;

public class PriceLevel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public bool IsSellerPrice { get; set; }
    
    public IEnumerable<PriceBook> PriceBooks { get; set; }
}