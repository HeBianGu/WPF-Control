// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace H.Controls.Form.PropertyItems;

public class TextPropertyViewItem : TextPropertyItem, IPropertyViewItem
{
    public TextPropertyViewItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    protected override string GetValue()
    {
        if (this.DisplayFormat == null)
            return base.GetValue();
        object value = this.PropertyInfo.GetValue(this.Obj);
        if (value == null)
            return null;
        if (IsTypeConverter(this.PropertyInfo))
            TypeConverterToString(value);

        var format = this.DisplayFormat.DataFormatString;
        if (string.IsNullOrWhiteSpace(format))
            return value?.ToString();

        var culture = CultureInfo.CurrentUICulture;
        // 1) 复合格式："{0:...}"
        if (format.Contains("{"))
        {
            try
            {
                return string.Format(culture, format, value);
            }
            catch
            {
                // 忽略并回退
                return value?.ToString();
            }
        }

        // 2) 纯格式说明符："F2"、"P2"、"yyyy-MM-dd" 等
        if (value is IFormattable formattable)
        {
            try
            {
                return formattable.ToString(format, culture);
            }
            catch
            {
                // 忽略并回退
                return value?.ToString();
            }
        }
        return (value?.ToString());
    }
}
