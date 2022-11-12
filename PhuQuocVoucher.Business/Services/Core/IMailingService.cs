using PhuQuocVoucher.Business.Dtos.MailDto;

namespace PhuQuocVoucher.Business.Services.Core;

public interface IMailingService
{
    public void SendEmailAsync(MailRequest mailRequest, bool isHtml);

    public Task SendEmailAsync(MailTemplateRequest templateRequest);
}