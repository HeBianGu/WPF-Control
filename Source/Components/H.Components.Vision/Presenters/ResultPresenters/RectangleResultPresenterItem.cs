// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
namespace H.Components.Vision.Presenters.ResultPresenters;

public interface IRectangleResultItem
{
    Rect Rect { get; }
}

public class RectangleResultPresenterItem : ResultPresenterItemBase, IRectangleResultItem, IShapeResultPresenterItem
{
    private readonly Rect _rect;
    public RectangleResultPresenterItem(Rect rect)
    {
        this.X = (int)rect.GetCenter().X;
        this.Y = (int)rect.GetCenter().Y;
        this.Area = (int)(rect.Width * rect.Height);
        this.Width = (int)rect.Width;
        this.Height = (int)rect.Height;
        this._rect = rect;
    }
    [Browsable(false)]
    public IShape Shape { get; set; }
    [Browsable(false)]
    public Rect Rect => this._rect;
    private double _x;
    [DataGridColumn("Auto", StringFormat = "{0:F2} px")]
    [Display(Name = "X中心", GroupName = "基础信息")]
    public double X
    {
        get { return _x; }
        set
        {
            _x = value;
            RaisePropertyChanged();
        }
    }

    private double _y;
    [DataGridColumn("Auto", StringFormat = "{0:F2} px")]
    [Display(Name = "Y中心", GroupName = "基础信息")]
    public double Y
    {
        get { return _y; }
        set
        {
            _y = value;
            RaisePropertyChanged();
        }
    }

    private double _width;
    [DataGridColumn("Auto", StringFormat = "{0:F2} px")]
    [Display(Name = "宽度", GroupName = "基础信息")]
    public double Width
    {
        get { return _width; }
        set
        {
            _width = value;
            RaisePropertyChanged();
        }
    }

    private double _height;
    [DataGridColumn("Auto", StringFormat = "{0:F2} px")]
    [Display(Name = "高度", GroupName = "基础信息")]
    public double Height
    {
        get { return _height; }
        set
        {
            _height = value;
            RaisePropertyChanged();
        }
    }

    private double _area;
    [DataGridColumn("Auto", StringFormat = "{0:F2} px")]
    [Display(Name = "面积", GroupName = "基础信息")]
    public double Area
    {
        get { return _area; }
        set
        {
            _area = value;
            RaisePropertyChanged();
        }
    }
}

