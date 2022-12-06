using System.Net.Mail;

namespace PhuQuocVoucher.Business.Dtos.MailDto;

public class MailRequest
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<Attachment>? Attachments { get; set; }
}