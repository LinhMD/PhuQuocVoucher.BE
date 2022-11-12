using CrudApiTemplate.Request;

namespace PhuQuocVoucher.Business.Dtos.PriceBookDto;

public class CreateListPriceBook
{
    public int VoucherId { get; set; }

    public IList<CreatePriceBookSimple> PriceBooks { get; set; }
}