using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Data.Dtos.ComboDto;

public class UpdateCombo : UpdateDto, IUpdateRequest<Combo>
{
    public string Name { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public double Price { get; set; }

    public IEnumerable<int> VoucherIds { get; set; }
}