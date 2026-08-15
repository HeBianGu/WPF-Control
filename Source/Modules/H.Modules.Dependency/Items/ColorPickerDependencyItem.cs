// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Dependency.Base;

namespace H.Modules.Dependency.Items;

[Display(Name = "ColorPicker", GroupName = "开源控件库", Description = "提供颜色选择控件。")]
public class ColorPickerDependencyItem : DependencyItemBase
{
    public ColorPickerDependencyItem()
    {
        Name = "ColorPicker";
        Author = "PixiEditor";
        Uri = "https://github.com/PixiEditor/ColorPicker";
        Version = "README 未注明";
        Licence = "MIT License";
        Description = "提供颜色选择控件。";
    }
}
