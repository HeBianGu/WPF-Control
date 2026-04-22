// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Message;
using H.Services.Project;

namespace H.App.AIDI.Extension;
public static class ProjectExtensions
{
    public static void AddLabelItem(this IProjectItem project, string labelName)
    {
        if (string.IsNullOrEmpty(labelName))
            return;
        if (project is AIDIProjectItem projectItem)
        {
            if (projectItem.Labels.Any(x => x.LabelName == labelName))
            {
                IocMessage.Snack.ShowInfo("已存在同名数据");
                return;
            }
            projectItem.Labels.Add(new ViewModel.LabelItem() { LabelName = labelName });
            projectItem.Save(out string message);
        }
    }
}
