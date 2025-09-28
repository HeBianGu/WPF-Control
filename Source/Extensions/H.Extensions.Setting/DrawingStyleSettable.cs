// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace H.Extensions.Setting;

public abstract class DrawingStyleSettable<T> : Settable<T> where T : new()
{
    private Brush _Stroke;
    [DefaultValue(null)]
    [Display(Name = "线条颜色", GroupName = "样式")]
    public Brush Stroke
    {
        get { return _Stroke; }
        set
        {
            _Stroke = value;
            RaisePropertyChanged();
        }
    }


    private double _StrokeThickness;
    [DefaultValue(1.0)]
    [Display(Name = "线条粗细", GroupName = "样式")]
    public double StrokeThickness
    {
        get { return _StrokeThickness; }
        set
        {
            _StrokeThickness = value;
            RaisePropertyChanged();
        }
    }


    private Brush _Fill;
    [DefaultValue(null)]
    [Display(Name = "填充色", GroupName = "样式")]
    public Brush Fill
    {
        get { return _Fill; }
        set
        {
            _Fill = value;
            RaisePropertyChanged();
        }
    }

}
