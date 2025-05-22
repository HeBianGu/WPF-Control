// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Markup;

namespace H.MarkupExtension;

[MarkupExtensionReturnType(typeof(Type))]
public class GenericTypeExtension : System.Windows.Markup.MarkupExtension
{
    public Type GenericType { get; set; }

    public Type TypeArgument { get; set; }

    public Type[] TypeArguments { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (this.TypeArgument == null)
            return this.GenericType.MakeGenericType(this.TypeArguments);
        return this.GenericType.MakeGenericType(this.TypeArgument);
    }
}
