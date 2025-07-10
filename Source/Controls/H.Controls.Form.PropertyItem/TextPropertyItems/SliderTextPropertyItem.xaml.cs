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
using System.Windows.Data;

namespace H.Controls.Form.PropertyItem.TextPropertyItems;

public abstract class SliderTextPropertyItemBase : TextPropertyItem
{
    protected SliderTextPropertyItemBase(PropertyInfo property, object obj) : base(property, obj)
    {
    }

    //private double _SliderValue;
    //public double SliderValue
    //{
    //    get { return _SliderValue; }
    //    set
    //    {
    //        _SliderValue = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //protected abstract void OnSliderValue(double v);
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

    public Type Type { get; set; } = typeof(T);

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

    private T _TickFrequency;
    public T TickFrequency
    {
        get { return _TickFrequency; }
        set
        {
            _TickFrequency = value;
            RaisePropertyChanged();
        }
    }
}

public class DoubleSliderTextPropertyItem : SliderTextPropertyItem<double>
{
    public DoubleSliderTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {
        this.TickFrequency = (this.Maximum - this.Minimum) / 100.0;
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Maximum = double.MaxValue;
        this.Minimum = double.MinValue;
        this.TickFrequency = (this.Maximum - this.Minimum) / 100.0;
    }
}

public class FloatSliderTextPropertyItem : SliderTextPropertyItem<float>
{
    public FloatSliderTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {
        this.TickFrequency = (this.Maximum - this.Minimum) / 100.0f;
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Maximum = float.MaxValue;
        this.Minimum = float.MinValue;
        this.TickFrequency = (this.Maximum - this.Minimum) / 100.0f;
    }
}

public class Int32SliderTextPropertyItem : SliderTextPropertyItem<int>
{
    public Int32SliderTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {
        this.TickFrequency = (int)((this.Maximum - this.Minimum) / 100.0);
        this.TickFrequency = Math.Max(1, this.TickFrequency);
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Maximum = int.MaxValue;
        this.Minimum = int.MinValue;
        this.TickFrequency = (this.Maximum) / 100;
    }

    protected override void SetValue(string value)
    {
        base.SetValue(value);
    }
}

public class GetValueFromStringValueConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return DependencyProperty.UnsetValue;
        if (value.TryChangeType(targetType, out object result))
            return result;
        return DependencyProperty.UnsetValue;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return DependencyProperty.UnsetValue;
        return value.TryConvertToString();
    }
}
