// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel;

namespace H.MarkupExtension;

/// <summary>
/// TypeConverter需继承TypeConverter
/// </summary>
public class GetTypeConverterExtension : GetValueToTypeExtensionBase
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        TypeConverter converter = Activator.CreateInstance(this.Type) as TypeConverter;
        return converter.ConvertFromString(this.Value);
    }
}

/// <summary>
/// ToType需要实现IConvertible
/// </summary>
public class GetIConvertibleExtension : GetValueToTypeExtensionBase
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Convert.ChangeType(this.Value, this.Type);
    }
}
