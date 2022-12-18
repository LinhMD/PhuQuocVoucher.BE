using System.ComponentModel.DataAnnotations;
using CrudApiTemplate.Request;
using PhuQuocVoucher.Data.Models;

namespace PhuQuocVoucher.Business.Dtos.BlogDto;

public class CreateBlog :  ICreateRequest<Blog>, IDto
{
    
    public DateTime CreateAt { get; } = DateTime.Now;

    public ModelStatus Status  { get; set; } =  ModelStatus.Active;
    
    [Required] public string Content { get; set; }

    [Required] public string BannerImage { get; set; }

    [Required] public string Title { get; set; }

    [Required] public string Summary { get; set; }

    public IEnumerable<int>? PlaceIds { get; set; }

    public IEnumerable<int>? TagIds { get; set; }
}