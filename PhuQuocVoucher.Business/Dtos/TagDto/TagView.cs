using CrudApiTemplate.View;
using Mapster;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.TagDto;

public class TagView : BaseModel, IView<Tag>, IDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public void InitMapper()
    {
        TypeAdapterConfig<Tag, TagView>.NewConfig();
    }
}