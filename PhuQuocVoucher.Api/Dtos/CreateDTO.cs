namespace PhuQuocVoucher.Api.Dtos;

public class CreateDTO : IDto
{
    public DateTime CreateTime { get; } = DateTime.Now;

    public virtual void InitMapper()
    {

    }
}