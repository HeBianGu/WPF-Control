// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Markup;

namespace H.Windows.Main;

public class GetWindowMaximizedMarginExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        Rect workArea = SystemParameters.WorkArea;
        Rect fullArea = new Rect(
            0,
            0,
            SystemParameters.PrimaryScreenWidth,
            SystemParameters.PrimaryScreenHeight);

        // 计算任务栏高度或宽度
        if (workArea.Bottom < fullArea.Bottom) // 任务栏在底部
        {
            return new Thickness(0, 0, 0, fullArea.Bottom - workArea.Bottom);
        }
        else if (workArea.Top > fullArea.Top) // 任务栏在顶部
        {
            return new Thickness(0, workArea.Top - fullArea.Top, 0, 0);
        }
        else if (workArea.Left > fullArea.Left) // 任务栏在左侧
        {
            return new Thickness(workArea.Left - fullArea.Left, 0, 0, 0);
        }
        else
        {
            return new Thickness(0, 0, fullArea.Right - workArea.Right, 0);
        }
    }
}
