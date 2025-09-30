// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Login;

public interface IRegistorOptions
{
    string Image { get; set; }
    string MailAccount { get; set; }
    string PrivacypolicyUri { get; set; }
    string ServiceAgreementUri { get; set; }
    bool UseMail { get; set; }
}