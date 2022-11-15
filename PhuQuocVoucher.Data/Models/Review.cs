namespace PhuQuocVoucher.Data.Models;

public class Review : BaseModel
{
    public int Id { get; set; }

    public byte Rating { get; set; }

    public string Comment { get; set; }

    public int? VoucherId { get; set; }

    public int? CustomerId { get; set; }

    public Customer? Customer { get; set; }
    
}