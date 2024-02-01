namespace H.Providers.Ioc
{
    public interface IMailLogService
    {
        bool Send(string subject, string body, out string message);
    }
}