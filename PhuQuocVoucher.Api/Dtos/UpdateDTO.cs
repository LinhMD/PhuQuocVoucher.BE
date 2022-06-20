namespace PhuQuocVoucher.Api.Dtos;

public class UpdateDTO : IDto
{
    public DateTime UpdateTime { get; } = DateTime.Now;
}