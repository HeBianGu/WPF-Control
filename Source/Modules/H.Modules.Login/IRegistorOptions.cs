namespace H.Modules.Login;

public interface IRegistorOptions
{
    string Image { get; set; }
    string MailAccount { get; set; }
    string PrivacypolicyUri { get; set; }
    string ServiceAgreementUri { get; set; }
    bool UseMail { get; set; }
}