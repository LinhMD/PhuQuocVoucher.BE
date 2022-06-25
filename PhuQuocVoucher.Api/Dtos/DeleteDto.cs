namespace PhuQuocVoucher.Api.Dtos;

public class DeleteDto : IDto
{
    public DateTime DeleteAt { get; set; }
    public virtual void InitMapper()
    {
    }
}