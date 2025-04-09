namespace H.Extensions.Mail;

public interface ISmtpSendOptions
{
    bool EnableSsl { get; set; }
    string Host { get; set; }
    bool IsBodyHtml { get; set; }
    string Password { get; set; }
    int Port { get; set; }
    string User { get; set; }
}