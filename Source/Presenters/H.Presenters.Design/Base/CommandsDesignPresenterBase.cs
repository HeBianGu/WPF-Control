// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.FontIcon;
using H.Extensions.Mvvm.Commands;
using H.Mvvm.Commands;
using H.Services.Message;
namespace H.Presenters.Design.Base;

public class CommandsDesignPresenterBase : CloneableDesignPresenterBase
{
    //[Icon(FontIcons.Refresh)]
    //[Display(Name = "恢复默认")]
    //public DisplayCommand DefaultCommand => new DisplayCommand(e =>
    //{
    //    if (e is string project)
    //    {

    //    }
    //});

    //[Icon(FontIcons.Save)]
    //[Display(Name = "保存模板")]
    //public DisplayCommand SaveTempateCommand => new DisplayCommand(e =>
    //{
    //    if (e is string project)
    //    {

    //    }
    //});

    [Icon(FontIcons.Edit)]    [Display(Name = "编辑")]
    public DisplayCommand EditCommand => new DisplayCommand(async e =>
    {
        await IocMessage.Form?.ShowTabEdit(this, null, null, x => x.UseCommand = false);
    });
}
