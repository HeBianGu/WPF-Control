// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.ShapeBox.State.Adds;

public class AddPointsShapeState<T> : PointsClickAddShapeState<T> where T : IPointsShape, new()
{
    public AddPointsShapeState()
    {
    }
    public AddPointsShapeState(IShapes shapes) : base(shapes)
    {
    }
    protected override void OnClick(Queue<Point> points)
    {
        this.Shape.Points = new PointCollection(points);
        this.RefreshStateShapes();
    }
    protected override void OnPreviewMouseMove(Point p)
    {
        if (_clickPoints.Count == 0)
            return;
        var points= _clickPoints.ToList();
        points.Add(p);
        this.Shape.Points = new PointCollection(points);
        this.RefreshStateShapes();
    }

    public override IShapeStyleSetting GetShapeStyleSetting()
    {
        return PolygonShapeStyleSetting.Instance;
    }
}

public class AddPolygonShapeState : AddPointsShapeState<PolygonShape>
{
    public AddPolygonShapeState()
    {
    }
    public AddPolygonShapeState(IShapes shapes) : base(shapes)
    {

    }
}
