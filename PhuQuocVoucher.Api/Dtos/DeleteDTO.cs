using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Api.Dtos;

public class DeleteDTO
{
    public DateTime DeleteTime { get; } = DateTime.Now;

    public ModelStatus Status { get; } = ModelStatus.Delete;
}