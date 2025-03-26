using H.Common.Attributes;
using H.Common.Commands;
using H.Services.Common.Feedback;
using H.Services.Mail;
using H.Services.Message;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Feedback;

[Icon("\xED15")]
[Display(Name = "用户反馈", Description = "显示用户反馈页面")]
public class ShowFeedbackCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        IFeedbackViewPresenter presenter = Ioc.GetService<IFeedbackViewPresenter>();
        bool? r = await IocMessage.Dialog?.Show(presenter);
        if (r != true)
            return;
        MailMessageItem messageItem = new MailMessageItem();
        messageItem.Subject = $"[{presenter.Title}] {presenter.Contact}";
        messageItem.Body = presenter.Text;
        messageItem.From = presenter.MailAccount;
        messageItem.To = new string[] { messageItem.From };
        messageItem.Attachments = presenter.Files.ToArray();
        string message = null;
        bool? mr = Ioc<IMailService>.Instance?.Send(messageItem, false, out message);
        if (mr == false)
            await IocMessage.ShowDialogMessage(message);
    }
}
