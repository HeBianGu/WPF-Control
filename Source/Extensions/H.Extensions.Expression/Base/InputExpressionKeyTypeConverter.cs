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

public class InputExpressionKeyTypeConverter : TypeConverter
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
            if (str == InheritanceInputExpressionKey.NAME)
                return new InheritanceInputExpressionKey();
            if (double.TryParse(str, out double dvalue))
                return new InputStringExpressionKey() { Value = str };
            int separatorIndex = str.IndexOf('.');
            if (separatorIndex >= 0)
            {
                int separatorCollectionIndex = str.IndexOf('[');
                if (separatorCollectionIndex > separatorIndex && str.EndsWith("]", StringComparison.Ordinal))
                {
                    string indexText = str[(separatorCollectionIndex + 1)..^1];
                    IInputExpressionKey indexKey;
                    if (indexText.Length == 0)
                        indexKey = null;
                    else if (int.TryParse(indexText, NumberStyles.Integer, culture, out int index))
                        indexKey = new InputStringExpressionKey() { Value = index };
                    else
                        indexKey = (IInputExpressionKey)this.ConvertFrom(context, culture, indexText);
                    return new CollectionInputExpressionKey()
                    {
                        GroupName = str[..separatorIndex],
                        Name = str[(separatorIndex + 1)..separatorCollectionIndex],
                        IndexKey = indexKey
                    };
                }
                return new InputExpressionKey()
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
            if (value is InheritanceInputExpressionKey inheritanceExpressionKey)
                return $"{InheritanceInputExpressionKey.NAME}";
            if (value is CollectionInputExpressionKey collectionExpressionKey)
                return collectionExpressionKey.DisplayName;
            if (value is InputExpressionKey key)
                return key.DisplayName;
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}

