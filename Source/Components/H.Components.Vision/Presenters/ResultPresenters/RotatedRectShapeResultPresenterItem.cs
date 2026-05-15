// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
namespace H.Components.Vision.Presenters.ResultPresenters;

public class RotatedRectShapeResultPresenterItem : ResultPresenterItemBase, IRectangleResultItem, IShapeResultPresenterItem
{
    private RotatedRectShape _shape;
    public RotatedRectShapeResultPresenterItem(RotatedRectShape shape)
    {
        this._shape = shape;
        this.Shape = shape;
        this.X = shape.Center.X;
        this.Y = shape.Center.Y;
        this.Width = shape.Size.Width;
        this.Height = shape.Size.Height;
        this.Area = shape.Size.Width * shape.Size.Height;
        this.Angle = shape.Angle;
        PointCollection points = new PointCollection(shape.GetPoints());
        points.Freeze();
        this.Points = points;
    }
    [Browsable(false)]
    public IShape Shape { get; set; }
    [Browsable(false)]
    public Rect Rect => this._shape.GetBoundingRect();

    private double _x;
    [DataGridColumn("*", StringFormat = "{0:F2} px")]
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
    [DataGridColumn("*", StringFormat = "{0:F2} px")]
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
    [DataGridColumn("*", StringFormat = "{0:F2} px")]
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
    [DataGridColumn("*", StringFormat = "{0:F2} px")]
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

    private double _Angle;
    [DataGridColumn("*", StringFormat = "{0:F2}°")]
    [Display(Name = "角度", GroupName = "基础信息")]
    public double Angle
    {
        get { return _Angle; }
        set
        {
            _Angle = value;
            RaisePropertyChanged();
        }
    }

    private double _Area;
    [DataGridColumn("*", StringFormat = "{0:F2}")]
    [Display(Name = "面积", GroupName = "基础信息")]
    public double Area
    {
        get { return _Area; }
        set
        {
            _Area = value;
            RaisePropertyChanged();
        }
    }

    private PointCollection _points;
    [DataGridColumn("*", StringFormat = "{0:F2} px")]
    [Display(Name = "坐标数据", GroupName = "基础信息")]
    public PointCollection Points
    {
        get { return _points; }
        set
        {
            _points = value;
            RaisePropertyChanged();
        }
    }
}

