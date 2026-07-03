// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.Controls.Diagram.Presenter.Expressions;

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
            var arr = str.Split('.');
            if (arr.Length == 2)
            {
                return new ExpressionKey() { GroupName = arr[0], Name = arr[1] };
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
            {
                return $"{primitiveExpressionKey.Value}";
            }
            if (value is InheritanceExpressionKey inheritanceExpressionKey)
            {
                return $"{InheritanceExpressionKey.NAME}";
            }
            if (value is ExpressionKey key)
            {
                return $"{key.GroupName}.{key.Name}";
            }
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}

