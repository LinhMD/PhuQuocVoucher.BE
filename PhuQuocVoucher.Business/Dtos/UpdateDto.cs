namespace PhuQuocVoucher.Business.Dtos;

public class UpdateDto : IDto
{
    public DateTime UpdateAt { get; } = DateTime.Now;

    public virtual void InitMapper()
    {
    }
}