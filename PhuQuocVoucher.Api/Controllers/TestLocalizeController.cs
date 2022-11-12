using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace PhuQuocVoucher.Api.Controllers;

[Route("api/v1/[controller]")]
public class TestLocalizeController : ControllerBase
{
    private readonly IStringLocalizer<TestLocalizeController> _localizer;

    public TestLocalizeController(IStringLocalizer<TestLocalizeController> localizer)
    {
        _localizer = localizer;
    }

    [HttpGet]
    public IActionResult Test()
    {
        return Ok(_localizer["email_test"].Value);
    }
}