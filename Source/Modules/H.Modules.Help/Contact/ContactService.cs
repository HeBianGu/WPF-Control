// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Modules.Help.Base;
using H.Services.Message;
using System.Diagnostics;

namespace H.Modules.Help.Contact;

public class ContactService : IContactService
{
    public async void Show()
    {
        await IocMessage.Form?.ShowView(ContactOptions.Instance);
    }
}
