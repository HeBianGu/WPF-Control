// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Mail;

public class MailMessageItem
{
    public string From { get; set; }
    public string[] To { get; set; }
    public string[] Cc { get; set; }
    public string[] Bcc { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string[] Attachments { get; set; }
}
