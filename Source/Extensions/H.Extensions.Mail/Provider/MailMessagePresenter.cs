using H.Providers.Ioc;
using H.Providers.Mvvm;

namespace H.Extensions.Mail
{
    public class MailMessagePresenter : ModelViewModel<MailMessageItem>
    {
        public MailMessagePresenter() : base(new MailMessageItem())
        {
            this.Model.From = SmtpSendOptions.Instance.User;
            this.Model.To = new string[] { SmtpSendOptions.Instance.User };
        }
        public MailMessagePresenter(MailMessageItem t) : base(t)
        {

        }
    }
}
