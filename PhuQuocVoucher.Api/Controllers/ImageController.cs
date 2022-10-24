using Microsoft.AspNetCore.Mvc;
using PhuQuocVoucher.Business.Services.Core;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IFirebaseServiceIntegration _firebase;

    public ImageController(IFirebaseServiceIntegration firebase)
    {
        _firebase = firebase;
    }

    [HttpPost]
    public async Task<ActionResult<string>> UploadImage(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var fileFormat = file.FileName.Split('.').Last();
        var fileName = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
        fileName = $"{fileName}_{Guid.NewGuid().ToString()}.{fileFormat}";

        return Ok(await _firebase.UploadFileAsync(stream, fileName));
    }
}