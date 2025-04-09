namespace H.Services.Mail;

public class MailMessageItem
{
    public string From { get; set; }
    public string[] To { get; set; }
    public string[] Cc { get; set; }
    public string[] Bcc { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string[] Attachments { get; set; }
}
