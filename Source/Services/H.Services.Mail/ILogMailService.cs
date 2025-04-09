namespace H.Services.Mail;

public interface ILogMailService
{
    bool Send(string subject, string body, out string message);
}