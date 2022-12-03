namespace PhuQuocVoucher.Data.Models;

public class PriceLevelT
{
    public int Id { get; set; }

    public bool IsAdult { get; set; }

    public PriceLevelType Type { get; set; }

    public string Name { get; set; }

    public double Rate { get; set; }

}