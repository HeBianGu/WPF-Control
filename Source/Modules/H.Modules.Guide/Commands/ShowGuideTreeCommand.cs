// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
namespace H.Modules.Guide.Commands;
[Icon("\xEC92")]
[Display(Name = "功能列表", Description = "显示版本功能列表")]
public class ShowGuideTreeCommand : ShowGuideTreeCommandBase
{
    protected override bool IsMatch(UIElement element)
    {
        return true;
    }
}
