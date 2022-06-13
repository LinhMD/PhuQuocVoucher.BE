using System.ComponentModel.DataAnnotations;

namespace PhuQuocVoucher.Data.Models;

public class VoucherType
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public VoucherType?  ParentType { get; set; }
}