// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
