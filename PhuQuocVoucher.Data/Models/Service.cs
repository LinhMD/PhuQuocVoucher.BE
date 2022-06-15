using System.ComponentModel.DataAnnotations;

namespace PhuQuocVoucher.Data.Models;

public class Service : BaseModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(2048)]
    public string Name { get; set; }

    public string Description { get; set; }

    public ServiceType Type { get; set; }

    public Place ServiceLocation { get; set; }

    public ServiceProvider Provider { get; set; }

    public int ProviderId { get; set; }


}