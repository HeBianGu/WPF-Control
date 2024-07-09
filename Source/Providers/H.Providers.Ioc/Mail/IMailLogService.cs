namespace H.Services.Common
{
    public interface IMailLogService
    {
        bool Send(string subject, string body, out string message);
    }
}