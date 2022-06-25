namespace PhuQuocVoucher.Api.Dtos;

public abstract class CreateDto : IDto
{
    public DateTime CreateAt { get; set; }

    public virtual void InitMapper()
    {

    }
}