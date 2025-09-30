// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Styles.Controls;

//https://learn.microsoft.com/zh-cn/windows/apps/design/style/segoe-ui-symbol-font?WT.mc_id=MVP_380318
public class FontIconTextBlock : TextBlock
{
    static FontIconTextBlock()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIconTextBlock), new FrameworkPropertyMetadata(typeof(FontIconTextBlock)));
    }
}
