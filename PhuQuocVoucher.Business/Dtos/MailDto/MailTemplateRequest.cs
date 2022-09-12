using Microsoft.AspNetCore.Http;

namespace PhuQuocVoucher.Business.Dtos.MailDto;

public class MailTemplateRequest
{
    public string FileTemplateName { get; set; }

    public IDictionary<string, string> values { get; set; }

    public MailRequest MailRequest { get; set; }
}