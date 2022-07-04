namespace PhuQuocVoucher.Api.Dtos.ComboDto;

public class UpdateCombo
{
    public string Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public double Price { get; set; }

    public IEnumerable<int> VoucherIds { get; set; }
}