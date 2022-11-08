using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PhuQuocVoucher.Data.Models;

public class PriceBook
{
    public int Id { get; set; }
    
    public PriceLevel PriceLevel { get; set; }
    
    public int PriceLevelId { get; set; }

    public Voucher Voucher { get; set; }

    public int VoucherId { get; set; }
    
    public double Price { get; set; }

}