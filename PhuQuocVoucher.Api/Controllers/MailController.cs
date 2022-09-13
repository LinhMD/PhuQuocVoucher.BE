
using PhuQuocVoucher.Business.Services;
using Microsoft.AspNetCore.Mvc;
using PhuQuocVoucher.Business.Dtos.MailDto;
using PhuQuocVoucher.Business.Services.Core;
using PhuQuocVoucher.Business.Services.Implements;

namespace PhuQuocVoucher.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]s")]
public class MailController : ControllerBase
{

    private readonly IMailingService _mailingService;

    public MailController(IMailingService mailingService)
    {
        _mailingService = mailingService;
    }

    [HttpGet("test")]
    public async Task<IActionResult> TestSendMail()
    {
        await _mailingService.SendEmailAsync(new MailTemplateRequest()
        {
            values = new Dictionary<string, string>()
            {
                {"UserName", "Linh Mai Dinh"},
                {"Mail", "maidinhlinh967@gmail.com"}
            },
            MailRequest = new MailRequest()
            {
                Subject = "test mail",
                ToEmail = "linhmaidinh1@gmail.com"
            },
            FileTemplateName = "HelloMail"
        });
        return Problem();
        return Ok();
    }
}