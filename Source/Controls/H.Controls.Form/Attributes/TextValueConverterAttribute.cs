// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.Attributes;

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
public class TextValueConverterAttribute : System.Attribute
{
    private readonly Type _type;
    private readonly IValueConverter _converter;
    private object _parameter;
    public TextValueConverterAttribute(Type type)
    {
        this._type = type;
        this._converter = Activator.CreateInstance(type) as IValueConverter;
    }
    public TextValueConverterAttribute(Type type, object parameter) : this(type)
    {
        this._parameter = parameter;
    }

    public IValueConverter ValueConverter => this._converter;

    public object Paremeter => this._parameter;


    public object Convert(object value)
    {
        return this.ValueConverter.Convert(value, typeof(string), this.Paremeter, System.Globalization.CultureInfo.CurrentUICulture);
    }
    public object ConvertBack(object value)
    {
        return this.ValueConverter.ConvertBack(value, typeof(string), this.Paremeter, System.Globalization.CultureInfo.CurrentUICulture);
    }
}
