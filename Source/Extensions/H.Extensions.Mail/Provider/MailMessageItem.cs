using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace H.Extensions.Mail
{
    public class MailMessageItem
    {
        public string From { get; set; } = SmtpSendOptions.Instance.User;
        public string[] To { get; set; } = { SmtpSendOptions.Instance.User };
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public FileInfo[] Attachments { get; set; }
    }
}
