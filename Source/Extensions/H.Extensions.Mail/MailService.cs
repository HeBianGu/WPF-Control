using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace H.Extensions.Mail;

public class MailService : IMailService
{
    public bool Send(MailMessageItem messageItem, bool isBodyHtml, out string message)
    {
        message = null;
        SmtpClient smtp = new SmtpClient()
        {
            Host = SmtpSendOptions.Instance.Host,
            Port = SmtpSendOptions.Instance.Port,
            EnableSsl = SmtpSendOptions.Instance.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(SmtpSendOptions.Instance.User, SmtpSendOptions.Instance.Password)
        };

        using MailMessage msg = new();
        msg.From = new MailAddress(messageItem.From);
        foreach (var to in messageItem.To)
        {
            msg.To.Add(new MailAddress(to));
        }

        if (messageItem.Cc != null)
        {
            foreach (var cc in messageItem.Cc)
            {
                msg.CC.Add(new MailAddress(cc));
            }
        }

        if (messageItem.Bcc != null)
        {
            foreach (var bcc in messageItem.Bcc)
            {
                msg.Bcc.Add(new MailAddress(bcc));
            }
        }

        msg.Subject = messageItem.Subject;
        msg.Body = messageItem.Body;
        msg.BodyEncoding = System.Text.Encoding.UTF8;
        msg.IsBodyHtml = isBodyHtml;
        msg.SubjectEncoding = System.Text.Encoding.UTF8;
        if (messageItem.Attachments != null)
        {
            foreach (var attachment in messageItem.Attachments)
            {
                Attachment data = new(attachment, MediaTypeNames.Application.Octet);
                var disposition = data.ContentDisposition;
                disposition!.CreationDate = File.GetCreationTime(attachment);
                disposition.ModificationDate = File.GetLastWriteTime(attachment);
                disposition.ReadDate = File.GetLastAccessTime(attachment);
                msg.Attachments.Add(data);
            }
        }
        try
        {
            smtp.Send(msg);
        }
        catch (System.Exception ex)
        {
            message = ex.Message;
            IocLog.Instance?.Error(ex);
            return false;
        }
        return true;
    }


}
