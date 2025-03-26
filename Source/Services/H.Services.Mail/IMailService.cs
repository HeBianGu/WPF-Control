
namespace H.Services.Mail;

public interface IMailService
{
    bool Send(MailMessageItem messageItem, bool isBodyHtml, out string message);
}