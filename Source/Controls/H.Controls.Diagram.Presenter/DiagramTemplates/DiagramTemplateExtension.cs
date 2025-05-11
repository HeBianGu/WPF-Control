// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.AppPath;

namespace H.Controls.Diagram.Presenter.DiagramTemplates;

public static class DiagramTemplateExtension
{
    public static string GetDefaultFileName(this IDiagramTempaltes templates)
    {
        return System.IO.Path.Combine(AppPaths.Instance.UserData, "diagramtemplates.json");
    }
}
