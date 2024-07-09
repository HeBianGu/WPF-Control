namespace H.Services.Mail
{
    public interface IMailLogService
    {
        bool Send(string subject, string body, out string message);
    }
}