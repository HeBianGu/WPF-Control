using H.Common.Attributes;
using H.Common.Commands;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Mail;

[Icon("\xE77F")]
[Display(Name = "发送邮件", Description = "将当前数据发送邮件")]
public class SendMailCommand : DisplayMarkupCommandBase
{
    public override void Execute(object parameter)
    {
        if (parameter is MailMessageItem messageItem)
            Ioc<IMailService>.Instance?.Send(messageItem, SmtpSendOptions.Instance.IsBodyHtml, out string message);

        if (parameter is MailMessagePresenter mailMessagePresenter)
            Ioc<IMailService>.Instance?.Send(mailMessagePresenter.Model, SmtpSendOptions.Instance.IsBodyHtml, out string message);

    }

    public override bool CanExecute(object parameter)
    {
        return Ioc<IMailService>.Instance != null;
    }
}
