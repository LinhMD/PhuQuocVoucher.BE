using Microsoft.AspNetCore.Mvc;
using PhuQuocVoucher.Business.Services.Core;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FirebaseController : ControllerBase
{
    private readonly IFirebaseServiceIntegration _firebase;

    public FirebaseController(IFirebaseServiceIntegration firebase)
    {
        _firebase = firebase;
    }

    [HttpPost("files")]
    public async Task<ActionResult<string>> UploadImage(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var fileFormat = file.FileName.Split('.').Last();
        var fileName = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
        fileName = $"{fileName}_{Guid.NewGuid().ToString()}.{fileFormat}";

        return Ok(await _firebase.UploadFileAsync(stream, fileName));
    }
}