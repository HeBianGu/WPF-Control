// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Mail;

public interface ISmtpSendOptions
{
    bool EnableSsl { get; set; }
    string Host { get; set; }
    bool IsBodyHtml { get; set; }
    string Password { get; set; }
    int Port { get; set; }
    string User { get; set; }
}