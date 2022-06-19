
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PhuQuocVoucher.Data.Models;

[Index(nameof(ProductId), IsUnique = true)]
[Index(nameof(VoucherName), IsUnique = true)]
public class Voucher : BaseModel
{

    public int Id { get; set; }
    public string VoucherName { get; set; }

    public double Price { get; set; }

    public int Inventory { get; set; }

    public int LimitPerDay { get; set; }

    public bool IsRequireProfileInfo { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }


    public Product Product { get; set; }

    public int ProductId { get; set; }

    public Service Service { get; set; }

    public IEnumerable<Combo> Combos { get; set; }

}