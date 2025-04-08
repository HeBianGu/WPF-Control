using H.Extensions.Mail;

namespace H.Modules.Feedback;

public class FeedBackMailService : MailService, IFeedBackMailService
{
    protected override ISmtpSendOptions GetSmtpSendOptions()
    {
        return FeedbackOptions.Instance;
    }
}
