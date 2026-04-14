// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Project;

[Obsolete]
public class GetProjectItemPresenterValueConverter : MarkupValueConverterBase
{
    public bool ReturnProjectItemOnNull { get; set; }
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IProjectItem projectItem && projectItem.Presenter != null)
            return projectItem.Presenter;
        if (this.ReturnProjectItemOnNull)
            return value;
        return null;
    }

}
