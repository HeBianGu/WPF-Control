namespace H.Extensions.Mail;

public class LogMailService : ILogMailService
{
    public bool Send(string subject, string body, out string message)
    {
        message = null;
        MailMessageItem mail = new MailMessageItem();
        mail.Subject = subject;
        mail.Body = body;
        mail.From = SmtpSendOptions.Instance.User;
        mail.To = new string[] { SmtpSendOptions.Instance.User };
        return Ioc<IMailService>.Instance?.Send(mail, SmtpSendOptions.Instance.IsBodyHtml, out message) == true;
    }
}
