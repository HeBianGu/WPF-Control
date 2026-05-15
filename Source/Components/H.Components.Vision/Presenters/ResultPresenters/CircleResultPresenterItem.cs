// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
namespace H.Components.Vision.Presenters.ResultPresenters;

public class CircleResultPresenterItem : ResultPresenterItemBase, IRectangleResultItem, IShapeResultPresenterItem
{
    public CircleResultPresenterItem(Point start, double radius)
    {
        this._start = start;
        this._radius = radius;
    }

    public CircleResultPresenterItem(int startX, int startY, int radius) : this(new Point(startX, startY), radius)
    {

    }

    [Browsable(false)]
    public IShape Shape { get; set; }

    [Browsable(false)]
    public Rect Rect => new Rect(this._start.X - this._radius, this._start.Y - this._radius, this._radius * 2, this._radius * 2);

    private Point _start;
    [DataGridColumn("*", StringFormat = "{0:F2}")]
    [Display(Name = "中心", GroupName = "基础信息")]
    public Point Start
    {
        get { return _start; }
        set
        {
            _start = value;
            RaisePropertyChanged();
        }
    }

    private double _radius;
    [DataGridColumn("*", StringFormat = "{0:F2} px")]
    [Display(Name = "半径", GroupName = "基础信息")]
    public double Radius
    {
        get { return _radius; }
        set
        {
            _radius = value;
            RaisePropertyChanged();
        }
    }
}

