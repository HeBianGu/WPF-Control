// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Mail;

public class LogMailService : ILogMailService
{
    public bool Send(string subject, string body, out string message)
    {
        message = null;
        MailMessageItem mail = new MailMessageItem();
        mail.Subject = subject;
        mail.Body = body;
        mail.From = SmtpSendOptions.Instance.User;
        mail.To = new string[] { SmtpSendOptions.Instance.User };
        return Ioc<IMailService>.Instance?.Send(mail, SmtpSendOptions.Instance.IsBodyHtml, out message) == true;
    }
}
