// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Attributes;
using H.Common.Commands;
using H.Extensions.Common;
using H.Extensions.FontIcon;
using H.Modules.Help.Base;
using H.Modules.Help.Contact;
using H.Presenters.Common.Presenters;
using H.Services.Message;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Help;

[Icon(FontIcons.Contact)]
[Display(Name = "联系方式", Description = "通过此方式联系到开发者")]
public class ShowContactCommand : ResxDisplayMarkupCommandBase
{
    public override Task ExecuteAsync(object parameter)
    {
        Ioc.GetService<IContactService>()?.Show();
        return base.ExecuteAsync(parameter);
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && Ioc.Exist<IContactService>();
    }
}

[Icon(FontIcons.Home)]
[Display(Name = "访问B站小店查看所有商品", Description = "访问B站小店查看所有商品")]
public class ShowBilibiliMailCommand : ShowContactCommand
{
    public override async Task ExecuteAsync(object parameter)
    {
        string bmail = "pack://application:,,,/H.Modules.Help;component/Assets/B站小店地址.png";
        await IocMessage.Dialog.ShowImageSource(bmail, x =>
        {
            x.Title = "扫一扫，进入B站小店查看所有商品";
            x.Width = 300;
            x.Height = 300;
            x.Padding = new Thickness(20);
        });
    }
}

[Icon(FontIcons.Home)]
[Display(Name = "访问Github", Description = "通过此方式联系到开发者")]
public class ShowGithubContactCommand : ShowContactCommand
{
    public override Task ExecuteAsync(object parameter)
    {
        ContactOptions.Instance.GitHub.ShowProcess();
        return Task.CompletedTask;
    }
}

[Icon(FontIcons.MailForward)]
[Display(Name = "发送邮件", Description = "通过此方式联系到开发者")]
public class ShowSendMailContactCommand : ShowContactCommand
{
    public override Task ExecuteAsync(object parameter)
    {
        ContactOptions.Instance.Email.ShowProcess();
        return Task.CompletedTask;
    }
}

[Icon(FontIcons.AddFriend)]
[Display(Name = "添加QQ", Description = "通过此方式联系到开发者")]
public class ShowQQContactCommand : ShowContactCommand
{
    public override Task ExecuteAsync(object parameter)
    {
        ContactOptions.Instance.QQ.ShowProcess();
        return Task.CompletedTask;
    }
}

[Icon(FontIcons.Contact)]
[Display(Name = "QQ", Description = "通过此方式联系到开发者")]
public class ShowQQCommand : AsyncMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.ShowImageSource(x =>
        {
            x.ImageSource = "pack://application:,,,/H.Modules.Help;component/Assets/QQ.jpg".ToImageSource();
        });
    }
}

[Icon(FontIcons.Website)]
[Display(Name = "访问博客", Description = "通过此方式联系到开发者")]
public class ShowBlogContactCommand : ShowContactCommand
{
    public override Task ExecuteAsync(object parameter)
    {
        ContactOptions.Instance.Blog.ShowProcess();
        return Task.CompletedTask;
    }
}

[Icon(FontIcons.Video)]
[Display(Name = "访问播客", Description = "通过此方式联系到开发者")]
public class ShowPodcastContactCommand : ShowContactCommand
{
    public override Task ExecuteAsync(object parameter)
    {
        ContactOptions.Instance.Podcast.ShowProcess();
        return Task.CompletedTask;
    }
}

[Icon(FontIcons.StatusCircleQuestionMark)]
[Display(Name = "提出问题", Description = "通过此方式向开源项目提出问题")]
public class ShowGitHubIssueContactCommand : ShowContactCommand
{
    public override Task ExecuteAsync(object parameter)
    {
        ContactOptions.Instance.GitHubIssue.ShowProcess();
        return Task.CompletedTask;
    }
}

