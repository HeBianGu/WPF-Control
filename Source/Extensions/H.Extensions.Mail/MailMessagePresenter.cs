// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels;

namespace H.Extensions.Mail;

public class MailMessagePresenter : ModelBindable<MailMessageItem>
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
