using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos;

public class CreateDto : IDto
{
    public DateTime CreateAt { get; set; }

    public ModelStatus Status { get; } = ModelStatus.Active;

    public virtual void InitMapper()
    {
    }
}