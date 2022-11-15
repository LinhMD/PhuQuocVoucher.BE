
using System.Net;
using System.Net.Mail;
using PhuQuocVoucher.Business.Dtos.MailDto;
using PhuQuocVoucher.Business.Services.Core;

namespace PhuQuocVoucher.Business.Services.Implements;

public class MailingService : IMailingService
{
    private readonly MailSetting _mailSettings;


    public MailingService(MailSetting mailSettings)
    {
        _mailSettings = mailSettings;
    }


    public  void SendEmailAsync(MailRequest mailRequest, bool isHtml = false)
    {
        var email = new MailMessage();
        email.Sender = new MailAddress(_mailSettings.Mail);
        email.From = new MailAddress(_mailSettings.Mail);
        email.To.Add(new MailAddress(mailRequest.ToEmail));
        email.Subject = mailRequest.Subject;
        if (mailRequest.Attachments != null)
        {
            foreach (var file in mailRequest.Attachments.Where(file => file.Length > 0))
            {
                email.Attachments.Add(new Attachment(file.Name));

            }
        }
        email.Body = mailRequest.Body;
        email.IsBodyHtml = isHtml;

        using SmtpClient client = new SmtpClient();
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
        client.Host = _mailSettings.Host;
        client.Port = _mailSettings.Port;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;

        client.Send(email);
    }

    public async Task SendEmailAsync(MailTemplateRequest templateRequest)
    {
        var filePath = Directory.GetCurrentDirectory() + $"\\MailTemplate\\{templateRequest.FileTemplateName}.html";
        using var str = new StreamReader(filePath);
        var mailText = await str.ReadToEndAsync();
        str.Close();

        mailText = templateRequest.values.Aggregate(mailText,
            (current, value) => current.Replace($"[{value.Key}]", value.Value));

        templateRequest.MailRequest.Body = mailText;

        SendEmailAsync(templateRequest.MailRequest, true);
    }


}