// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
global using H.Controls.ShapeBox.Shapes.Base;
global using H.Extensions.Common;

namespace H.Components.Vision.Presenters.ResultPresenters;

public class PointsShapeResultPresenterItem : ResultPresenterItemBase, IRectangleResultItem, IShapeResultPresenterItem
{
    private IPointsShape _shape;
    public PointsShapeResultPresenterItem(IPointsShape shape)
    {
        this._shape = shape;
        this.Shape = shape;
        this.X = shape.BoundingBox.GetCenter().X;
        this.Y = shape.BoundingBox.GetCenter().Y;
        this.Width = shape.BoundingBox.Width;
        this.Height = shape.BoundingBox.Height;
        this.Area = shape.Area;
        this.Points = shape.Points;
    }
    [Browsable(false)]
    public IShape Shape { get; set; }
    [Browsable(false)]
    public Rect Rect => this._shape.BoundingBox;

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

    private Points _points;
    [DataGridColumn("*", StringFormat = "{0:F2} px")]
    [Display(Name = "坐标数据", GroupName = "基础信息")]
    public Points Points
    {
        get { return _points; }
        set
        {
            _points = value;
            RaisePropertyChanged();
        }
    }
}

