// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Form.PropertyItems;

public class TextPropertyItem : ObjectPropertyItem<string>
{
    public TextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {
        TextBoxAttribute ta = property.GetCustomAttribute<TextBoxAttribute>();
        if (ta != null)
        {
            this.TextWrapping = ta.TextWrapping;
            this.UseClear = ta.UseClear;
        }
    }

    private TextWrapping _textWrapping = TextWrapping.Wrap;
    /// <summary> 说明  </summary>
    public TextWrapping TextWrapping
    {
        get { return _textWrapping; }
        set
        {
            _textWrapping = value;
            RaisePropertyChanged();
        }
    }

    private bool _useClear;
    /// <summary> 说明  </summary>
    public bool UseClear
    {
        get { return _useClear; }
        set
        {
            _useClear = value;
            RaisePropertyChanged();
        }
    }

    protected override string GetValue()
    {
        object value = this.PropertyInfo.GetValue(this.Obj);
        return value == null ? null : IsTypeConverter(this.PropertyInfo) ? TypeConverterToString(value) : (value?.ToString());
    }

    public static bool IsIConvertible(PropertyInfo info)
    {
        return typeof(IConvertible).IsAssignableFrom(info.PropertyType);
    }

    public static bool IsTypeConverter(PropertyInfo info)
    {
        TypeConverterAttribute propertyConvert = info.GetCustomAttribute<TypeConverterAttribute>();
        if (propertyConvert != null)
            return true;
        TypeConverterAttribute typeConvert = info.PropertyType.GetCustomAttribute<TypeConverterAttribute>();
        return typeConvert != null;
    }

    private string TypeConverterToString(object value)
    {
        TypeConverterAttribute propertyConvert = this.PropertyInfo.GetCustomAttribute<TypeConverterAttribute>();
        if (propertyConvert != null)
        {
            Type type = Type.GetType(propertyConvert.ConverterTypeName);
            TypeConverter ddd = Activator.CreateInstance(type) as TypeConverter;
            return ddd.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, value);
        }

        TypeConverterAttribute typeConvert = this.PropertyInfo.PropertyType.GetCustomAttribute<TypeConverterAttribute>();
        if (typeConvert != null)
        {
            Type type = Type.GetType(typeConvert.ConverterTypeName);
            ConstructorInfo constructor = type.GetConstructors().FirstOrDefault(l => l.GetParameters().Count() == 0);
            if (constructor != null)
            {
                TypeConverter instance = Activator.CreateInstance(type) as TypeConverter;
                if (value != null)
                    return instance.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, value);
            }
        }
        return value?.ToString();
    }

    //[Browsable(true)]
    //[Display(Name = "恢复默认", Icon = "\xe8dc", Order = 1)]
    //public override RelayCommand LoadDefaultCommand => new RelayCommand(l =>
    //{
    //    this.LoadDefault();
    //    this.LoadValue();
    //});

    //[Browsable(true)]
    //[Display(Name = "删除", Icon = "\xe857", Order = 0)]
    //public RelayCommand ClearCommand => new RelayCommand(l =>
    //{
    //    this.Value = null;
    //});
}
