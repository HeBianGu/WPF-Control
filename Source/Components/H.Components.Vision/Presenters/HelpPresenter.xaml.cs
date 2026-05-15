// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Vision.Presenters;
public interface IHelpPresenter
{
}

public class HelpPresenter : IHelpPresenter
{
    public string Name { get; set; } = "更多 >>";
    public string Content { get; set; } = "开发文档地址";
    public string Url { get; set; } = "https://hebiangu.github.io/WPF-Control-Docs/";
}
