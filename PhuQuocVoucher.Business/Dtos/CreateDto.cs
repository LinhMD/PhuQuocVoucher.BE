using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos;

public class CreateDto : IDto
{
    public DateTime CreateAt { get; } = DateTime.Now;

    public ModelStatus Status { get; set; } =  ModelStatus.Active;

    public virtual void InitMapper()
    {
    }
}