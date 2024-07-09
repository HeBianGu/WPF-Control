namespace H.Providers.Ioc
{
    public class ShowFeedbackCommand : IocMarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            IFeedbackViewPresenter presenter = System.Ioc.GetService<IFeedbackViewPresenter>();
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
}
