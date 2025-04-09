// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Xml.Serialization;

namespace H.Presenters.Design.Base;

[Display(Name = "列头设置")]
public class ColumnPropertyInfo : BindableBase
{
    public ColumnPropertyInfo(PropertyInfo t)
    {
        DisplayAttribute display = t.GetCustomAttribute<DisplayAttribute>();
        this.Header = display?.Name ?? t.Name;
        this.PropertyInfo = t;
    }
    [System.Text.Json.Serialization.JsonIgnore]

    [XmlIgnore]
    [Browsable(false)]
    public PropertyInfo PropertyInfo { get; }

    private string _header;
    [Display(Name = "列名")]
    public string Header
    {
        get { return _header; }
        set
        {
            _header = value;
            RaisePropertyChanged();
        }
    }

    //private double _width = double.NaN;
    //[Display(Name = "列宽")]
    //public double Width
    //{
    //    get { return _width; }
    //    set
    //    {
    //        _width = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private int _displayIndex;
    //[Display(Name = "排序")]
    //public int DisplayIndex
    //{
    //    get { return _displayIndex; }
    //    set
    //    {
    //        _displayIndex = value;
    //        RaisePropertyChanged();
    //    }
    //}

    private bool _isVisible;
    [Display(Name = "显示")]
    public bool IsVisible
    {
        get { return _isVisible; }
        set
        {
            _isVisible = value;
            RaisePropertyChanged();
        }
    }

    public override string ToString()
    {
        string v = this.IsVisible ? "[显示]" : "[隐藏]";
        return $"列名：{v} {this.Header}";
    }
}
