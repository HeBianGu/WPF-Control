// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Markup;

namespace H.MarkupExtension;

public class StaticPropertyExtension : StaticExtension
{
    public StaticPropertyExtension()
    {

    }
    public StaticPropertyExtension(string member) : base(member)
    {

    }
    public string PropertyName { get; set; }
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (string.IsNullOrEmpty(this.PropertyName))
            return base.ProvideValue(serviceProvider);
        var obj = base.ProvideValue(serviceProvider);
        return obj.GetType().GetProperty(this.PropertyName).GetValue(obj);
    }
}
