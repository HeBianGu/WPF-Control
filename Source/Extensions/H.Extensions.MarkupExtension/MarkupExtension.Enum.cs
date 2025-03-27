// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace H.Extensions.MarkupExtension;

public class GetEnumSourceExtension : System.Windows.Markup.MarkupExtension
{
    private Type _enumType;

    public Type EnumType
    {
        get { return this._enumType; }
        set
        {
            if (value != this._enumType)
            {
                if (null != value)
                {
                    Type enumType = Nullable.GetUnderlyingType(value) ?? value;

                    if (!enumType.IsEnum)
                        throw new ArgumentException("Type must be for an Enum.");
                }
                this._enumType = value;
            }
        }
    }

    public GetEnumSourceExtension()
    {

    }

    public GetEnumSourceExtension(Type enumType)
    {
        this.EnumType = enumType;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (null == this._enumType)
            throw new InvalidOperationException("This EnumType must be specified.");
        Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;
        Array enumVlues = Enum.GetValues(actualEnumType);

        if (actualEnumType == this._enumType)
            return enumVlues;

        Array tempArray = Array.CreateInstance(actualEnumType, enumVlues.Length + 1);

        enumVlues.CopyTo(tempArray, 1);

        return tempArray;


    }
}

/// <summary> 根据DisplayAttribute特性中组名显示选项 </summary>
public class GetEnumGroupSourceExtension : System.Windows.Markup.MarkupExtension
{
    private Type _enumType;

    public Type EnumType
    {
        get { return this._enumType; }
        set
        {
            if (value != this._enumType)
            {
                if (null != value)
                {
                    Type enumType = Nullable.GetUnderlyingType(value) ?? value;

                    if (!enumType.IsEnum)
                        throw new ArgumentException("Type must be for an Enum.");
                }
                this._enumType = value;
            }
        }
    }

    public string GroupName { get; set; }

    public GetEnumGroupSourceExtension()
    {

    }

    public GetEnumGroupSourceExtension(Type enumType)
    {
        this.EnumType = enumType;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (null == this._enumType)
            throw new InvalidOperationException("This EnumType must be specified.");
        Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;

        string[] names = Enum.GetNames(actualEnumType);

        List<string> matchs = new List<string>();

        foreach (string item in names)
        {
            FieldInfo field = actualEnumType.GetField(item);

            if (field == null) continue;

            DisplayAttribute display = field.GetCustomAttribute<DisplayAttribute>();

            if (display == null)
                continue;
            if (display.GroupName == null)
                continue;
            if (display.GroupName.Split(',').Contains(this.GroupName) == true)
            {
                matchs.Add(item);
            }
        }

        return matchs.Select(l => Enum.Parse(actualEnumType, l));
    }
}

public class GetEnumExtension : GetValueToTypeExtensionBase
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Enum.Parse(this.Type, this.Value);
    }
}
