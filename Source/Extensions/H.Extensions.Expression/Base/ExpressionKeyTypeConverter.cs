// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Expressions.Keys;
using System.ComponentModel;
using System.Globalization;

namespace H.Extensions.Expressions.Base;

public class ExpressionKeyTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string))
            return true;
        var r = base.CanConvertFrom(context, sourceType);
        return r;
    }
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string str)
        {
            if (str == InheritanceExpressionKey.NAME)
                return new InheritanceExpressionKey();
            if (double.TryParse(str, out double dvalue))
                return new InputStringExpressionKey() { Value = str };
            int separatorIndex = str.IndexOf('.');
            if (separatorIndex >= 0)
            {
                return new ExpressionKey()
                {
                    GroupName = str[..separatorIndex],
                    Name = str[(separatorIndex + 1)..]
                };
            }
            else
            {
                return new InputStringExpressionKey() { Value = str };
            }
        }
        return base.ConvertFrom(context, culture, value);
    }
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string))
        {
            if (value is InputStringExpressionKey primitiveExpressionKey)
                return $"{primitiveExpressionKey.Value}";
            if (value is InheritanceExpressionKey inheritanceExpressionKey)
                return $"{InheritanceExpressionKey.NAME}";
            if (value is ExpressionKey key)
                return $"{key.GroupName}.{key.Name}";
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}

