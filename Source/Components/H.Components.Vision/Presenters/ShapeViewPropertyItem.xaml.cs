// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItems.Base;

namespace H.Components.Vision.Presenters;
public class ShapeViewPropertyItem : ObjectPropertyItem<object>
{
    public ShapeViewPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    protected override object GetValue()
    {
        var value = base.GetValue();
        this.Shapes = this.GetShapes(value).ToObservable();
        return value;
    }

    protected virtual IEnumerable<IShape> GetShapes(object value)
    {
        if (value is Point[][] pointss)
        {
            foreach (var item in pointss.Select(x => x.ToPolygonShape()))
            {
                yield return item;
            }
        }

        if (value is IEnumerable<Point> points)
            yield return points.ToPolygonShape();
        if (value is Rect rect)
            yield return rect.ToRectShape();
        if (value is Point point)
            yield return point.ToPointShape();
    }

    private ObservableCollection<IShape> _Shapes = new ObservableCollection<IShape>();
    public ObservableCollection<IShape> Shapes
    {
        get { return _Shapes; }
        set
        {
            _Shapes = value;
            RaisePropertyChanged();
        }
    }
}

