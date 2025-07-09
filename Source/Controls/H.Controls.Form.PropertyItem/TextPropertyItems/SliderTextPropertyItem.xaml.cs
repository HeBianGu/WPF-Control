// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.ValueConverter;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows;

namespace H.Controls.Form.PropertyItem.TextPropertyItems;

public class SliderTextPropertyItem : TextPropertyItem
{
    public SliderTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {
        var range = property.GetCustomAttribute<RangeAttribute>();
        if (range == null)
            return;
        this.Maximum = (double)range.Maximum;
        this.Minimum = (double)range.Minimum;
    }

    private double _Maximum = double.MaxValue;
    public double Maximum
    {
        get { return _Maximum; }
        set
        {
            _Maximum = value;
            RaisePropertyChanged();
        }
    }

    private double _Minimum = double.MinValue;
    public double Minimum
    {
        get { return _Minimum; }
        set
        {
            _Minimum = value;
            RaisePropertyChanged();
        }
    }


}

public class GetDoubleFromStringValueConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return DependencyProperty.UnsetValue;
        double d = value.TryChangeType<double>();
        return d;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return DependencyProperty.UnsetValue;
        return value.TryConvertToString();
    }
}
