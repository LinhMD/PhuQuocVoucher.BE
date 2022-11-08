using System.ComponentModel.DataAnnotations;

namespace PhuQuocVoucher.Data.Models;

public class ServiceType : BaseModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public ServiceType?  ParentType { get; set; }

    public int? ParentTypeId { get; set; }
    
    public double?  DefaultCommissionRate { get; set; }
    
}