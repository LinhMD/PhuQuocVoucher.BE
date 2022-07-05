namespace PhuQuocVoucher.Data.Dtos;

public class UpdateDto : IDto
{
    public DateTime UpdateAt { get; } = DateTime.Now;
    public virtual void InitMapper()
    {

    }
}