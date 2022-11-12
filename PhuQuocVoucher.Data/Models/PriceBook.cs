using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace PhuQuocVoucher.Data.Models;

public class PriceBook
{
    public int Id { get; set; }
    
    public PriceLevel PriceLevel { get; set; }
    

    [JsonIgnore]
    public Voucher Voucher { get; set; }

    public int VoucherId { get; set; }
    
    public double Price { get; set; }

}