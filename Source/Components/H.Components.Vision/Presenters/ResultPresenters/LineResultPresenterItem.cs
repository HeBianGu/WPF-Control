// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
namespace H.Components.Vision.Presenters.ResultPresenters;

public interface ILineResultItem
{
    Point Start { get; }
    Point End { get; }
}

public class LineResultPresenterItem : ResultPresenterItemBase, ILineResultItem, IRectangleResultItem, IShapeResultPresenterItem
{
    private readonly Rect _rect;
    public LineResultPresenterItem(Point start, Point end)
    {
        this._start = start;
        this._end = end;
        this._rect = new Rect(start, end);
        this.Distance = (end - start).Length;
    }

    public LineResultPresenterItem(int startX, int startY, int endX, int endY) : this(new Point(startX, startY), new Point(endX, endY))
    {

    }
    [Browsable(false)]
    public IShape Shape { get; set; }
    [Browsable(false)]
    public Rect Rect => this._rect;

    private Point _start;
    [DataGridColumn("*", StringFormat = "{0:F2} px")]
    [Display(Name = "起点", GroupName = "基础信息")]
    public Point Start
    {
        get { return _start; }
        set
        {
            _start = value;
            RaisePropertyChanged();
        }
    }

    private Point _end;
    [DataGridColumn("*", StringFormat = "{0:F2} px")]
    [Display(Name = "终点", GroupName = "基础信息")]
    public Point End
    {
        get { return _end; }
        set
        {
            _end = value;
            RaisePropertyChanged();
        }
    }

    private double _distance;
    [DataGridColumn("*", StringFormat = "{0:F2} px")]
    [Display(Name = "距离", GroupName = "基础信息")]
    public double Distance
    {
        get { return _distance; }
        set
        {
            _distance = value;
            RaisePropertyChanged();
        }
    }

    [DataGridColumn("*", StringFormat = "{0:F2}°")]
    [Display(Name = "角度", GroupName = "基础信息")]
    public double Angle => this.CalculateAngle(this.Start.X, this.Start.Y, this.End.X, this.End.Y);


    private double CalculateAngle(double x1, double y1, double x2, double y2)
    {
        double deltaY = y2 - y1;
        double deltaX = x2 - x1;
        double angleInRadians = Math.Atan2(deltaY, deltaX);
        double angleInDegrees = angleInRadians * (180 / Math.PI);
        if (angleInDegrees < 0)
            angleInDegrees += 360;
        return angleInDegrees;
    }
}

