using H.Extensions.Mail;

namespace H.Extensions.Mail
{
    public interface IMailService
    {
        bool Send(MailMessageItem messageItem, bool isBodyHtml, out string message);
    }
}