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

public abstract class SliderTextPropertyItemBase : TextPropertyItem
{
    protected SliderTextPropertyItemBase(PropertyInfo property, object obj) : base(property, obj)
    {
    }
}

public class SliderTextPropertyItem<T> : SliderTextPropertyItemBase
{
    public SliderTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {
        var range = property.GetCustomAttribute<RangeAttribute>();
        if (range == null)
            return;
        this.Maximum = range.Maximum.TryChangeType<T>();
        this.Minimum = range.Minimum.TryChangeType<T>();
    }

    private T _Maximum;
    public T Maximum
    {
        get { return _Maximum; }
        set
        {
            _Maximum = value;
            RaisePropertyChanged();
        }
    }

    private T _Minimum;
    public T Minimum
    {
        get { return _Minimum; }
        set
        {
            _Minimum = value;
            RaisePropertyChanged();
        }
    }

    private T _SmallChange;
    public T SmallChange
    {
        get { return _SmallChange; }
        set
        {
            _SmallChange = value;
            RaisePropertyChanged();
        }
    }

}

public class DoubleSliderTextPropertyItem: SliderTextPropertyItem<double>
{
    public DoubleSliderTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {
       
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Maximum = double.MaxValue;
        this.Minimum = double.MinValue;
        this.SmallChange = (this.Maximum - this.Minimum) / 100.0;
    }
}

public class FloatSliderTextPropertyItem : SliderTextPropertyItem<float>
{
    public FloatSliderTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Maximum = float.MaxValue;
        this.Minimum = float.MinValue;
        this.SmallChange = (this.Maximum - this.Minimum) / 100.0f;
    }
}

public class Int32SliderTextPropertyItem : SliderTextPropertyItem<int>
{
    public Int32SliderTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Maximum = int.MaxValue;
        this.Minimum = int.MinValue;
        this.SmallChange = (int)((this.Maximum - this.Minimum) / 100.0);
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
