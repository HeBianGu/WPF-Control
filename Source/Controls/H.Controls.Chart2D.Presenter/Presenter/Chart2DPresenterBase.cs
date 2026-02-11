// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


using H.Extensions.Mvvm.ViewModels.Base;
using H.Presenters.Design.Base;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Controls.Chart2D.Presenter.Presenter;
public class Chart2DPresenterBase : CloneableDesignPresenterBase, ICloneable
{
    public Chart2DPresenterBase()
    {
        this.MinHeight = 200.0;
    }

    //public override void LoadDefault()
    //{
    //    base.LoadDefault();
    //    this.Foreground = Application.Current.FindResource(BrushKeys.Foreground) as Brush;
    //}

    protected static Random random = new Random();

    private bool _drawOnce;
    [Browsable(false)]
    [JsonIgnore]

    [XmlIgnore]
    public bool drawOnce
    {
        get { return _drawOnce; }
        set
        {
            _drawOnce = value;
            RaisePropertyChanged();
        }
    }


    private DoubleCollection _data = new DoubleCollection();
    [Display(Name = "数据值", GroupName = "常用,数据")]
    public DoubleCollection Data
    {
        get { return _data; }
        set
        {
            _data = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<string> _xDisplay = new ObservableCollection<string>();
    [Display(Name = "显示名称", GroupName = "常用,数据")]
    [TypeConverter(typeof(DisplayTypeConverter))]
    public ObservableCollection<string> xDisplay
    {
        get { return _xDisplay; }
        set
        {
            _xDisplay = value;
            RaisePropertyChanged();
        }
    }

    private bool _usexAxis = true;
    [Display(Name = "显示x坐标", GroupName = "常用,样式")]
    public bool UsexAxis
    {
        get { return _usexAxis; }
        set
        {
            _usexAxis = value;
            RaisePropertyChanged();
        }
    }


    private bool _useyAxis = true;
    [Display(Name = "显示y坐标", GroupName = "常用,样式")]
    public bool UseyAxis
    {
        get { return _useyAxis; }
        set
        {
            _useyAxis = value;
            RaisePropertyChanged();
        }
    }


    private bool _useGrid = true;
    [Display(Name = "显示网格", GroupName = "常用,样式")]
    public bool UseGrid
    {
        get { return _useGrid; }
        set
        {
            _useGrid = value;
            RaisePropertyChanged();
        }
    }

    private bool _UseLegend = true;
    [Display(Name = "显示图例", GroupName = "常用,样式")]
    public bool UseLegend
    {
        get { return _UseLegend; }
        set
        {
            _UseLegend = value;
            RaisePropertyChanged();
        }
    }


    protected IEnumerable<double> Load(IEnumerable<double> data, double max = double.PositiveInfinity, double min = 0.0, int count = 5)
    {
        if (data.Count() == 0)
            yield break;
        max = max == double.PositiveInfinity ? data.Max() : max;
        min = min == double.PositiveInfinity ? data.Min() : min;
        double vSpan = (max - min) / count;
        double current = min;
        while (max - current > -0.0001)
        {
            yield return current;
            double ssss = max - current;
            if (ssss <= 0)
                break;
            current += vSpan;
        }
    }
}
