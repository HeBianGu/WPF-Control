// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
namespace H.Components.Vision.Presenters.ResultPresenters;

public class PointResultPresenterItem : ResultPresenterItemBase, IRectangleResultItem, IShapeResultPresenterItem
{
    public PointResultPresenterItem(Point start)
    {
        this.X = start.X;
        this.Y = start.Y;
    }
    [Browsable(false)]
    public IShape Shape { get; set; }
    [Browsable(false)]
    public Rect Rect => new Rect(this.X - 10, this.Y - 10, 10 * 2, 10 * 2);

    private double _x;
    [DataGridColumn("*", StringFormat = "{0:F2} px")]
    [Display(Name = "X坐标", GroupName = "基础信息")]
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
    [Display(Name = "Y坐标", GroupName = "基础信息")]
    public double Y
    {
        get { return _y; }
        set
        {
            _y = value;
            RaisePropertyChanged();
        }
    }
}

