// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Interfaces;
global using H.Controls.Diagram.Presenter.NodeDatas.Base;
global using H.Mvvm.ViewModels.Base;
using H.Components.Vision.Geometrys;
using H.Components.Vision.Presenters.ResultPresenters;
using H.Controls.ShapeBox.Shapes.Base;
using System;

namespace H.VisionMaster.ResultPresenter;

public static partial class ResultPresenterExtension
{
    //public static IResultPresenter ToValueResultPresenter<T>(this T value, Action<ValueResultPresenterItem<T>> action = null)
    //{
    //    return value.ToValueResultPresenterItem(action).ToDataGridViewResultData();
    //}

    public static IResultPresenter ToValueResultPresenter<T>(this T value)
    {
        return new ValueResultPresenter<T>(value);
    }

    private static ValueResultPresenterItem<T> ToValueResultPresenterItem<T>(this T value, Action<ValueResultPresenterItem<T>> action = null)
    {
        ValueResultPresenterItem<T> r = new ValueResultPresenterItem<T>() { Value = value };
        action?.Invoke(r);
        return r;
    }

    private static IEnumerable<ValueResultPresenterItem<string>> ToValueResultPresenterItems<T>(this IEnumerable<T> values, Func<T, ValueResultPresenterItem<string>> selector = null)
    {
        foreach (T value in values)
        {
            yield return selector?.Invoke(value);
        }
    }

    private static IEnumerable<ValueResultPresenterItem<T>> ToValueResultPresenterItems<T>(this IEnumerable<T> values, Func<T, ValueResultPresenterItem<T>> selector = null)
    {
        foreach (T value in values)
        {
            yield return selector?.Invoke(value);
        }
    }

    private static IEnumerable<ValueResultPresenterItem<T>> ToValueResultPresenterItems<T>(this IEnumerable<T> values, Action<T, ValueResultPresenterItem<T>> action = null)
    {
        foreach (T value in values)
        {
            ValueResultPresenterItem<T> item = new ValueResultPresenterItem<T>(value);
            action?.Invoke(value, item);
            yield return item;
        }
    }

    private static IResultPresenter ToDataGridValueResultPresenter<T>(this IEnumerable<T> values, Action<T, ValueResultPresenterItem<T>> action = null)
    {
        return values.ToValueResultPresenterItems(action).ToResultPresenter();
    }

    private static IResultPresenter ToDataGridValueResultPresenter<T>(this IEnumerable<T> values, Func<T, ValueResultPresenterItem<T>> selector = null)
    {
        return values.ToValueResultPresenterItems(selector).ToResultPresenter();
    }

    private static IResultPresenter ToDataGridValueResultPresenter<T>(this IEnumerable<T> values, Func<T, ValueResultPresenterItem<string>> selector = null)
    {
        return values.ToValueResultPresenterItems(selector).ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter<T>(this IEnumerable<T> values) where T : IResultPresenterItem
    {
        return new DataGridResultPresenter<T>(values);
    }

    //public static IResultPresenter ToDataGridResultPresenter<T>(this T value) where T : IResultPresenterItem
    //{
    //    return new DataGridResultPresenter<T>(new ObservableCollection<T>() { value });
    //}

    public static IResultPresenter ToDataGridValueResultPresenter<T>(this IEnumerable<T> values, Func<T, string> valueSelector, Func<T, string> nameSelector)
    {
        return values.ToDataGridValueResultPresenter(x => new ValueResultPresenterItem<string>() { Name = nameSelector?.Invoke(x), Value = valueSelector?.Invoke(x) });
    }

    public static IResultPresenter ToDataGridValueResultPresenter<T>(this T value, Func<T, string> nameSelector, Func<T, string> valueSelector) where T : INodeData
    {
        ObservableCollection<T> values = new ObservableCollection<T>() { value };
        Func<T, string> valueString = x => valueSelector?.Invoke(value) ?? value?.ToString();
        return values.ToDataGridValueResultPresenter(valueString, nameSelector);
    }

    public static IResultPresenter ToResultPresenter<T>(this INameable nodeData, T value)
    {
        return value.ToEnumerable().ToDataGridValueResultPresenter(x => x?.ToString(), x => nodeData.Name);
    }

    public static IResultPresenter ToDataGridValueResultPresenter(this object value, string name)
    {
        return value.ToEnumerable().ToDataGridValueResultPresenter(x => x.ToString(), x => name);
    }

    public static IResultPresenter ToResultPresenter(this IResultPresenterItem value)
    {
        return value.ToEnumerable().ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<Rect> values, Func<Rect, string> nameSelector)
    {
        return values.Select(x => new RectangleResultPresenterItem(x) { Name = nameSelector?.Invoke(x) }).ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<Point> values)
    {
        return values.ToDataGridValueResultPresenter(x => x.ToString(), x => "位置信息");
    }

    public static IResultPresenter ToRectangleDataGridResultPresenter<T>(this IEnumerable<T> values, Func<T, Rect> valueSelector, Func<T, string> nameSelector)
    {
        return values.Select(x => new RectangleResultPresenterItem(valueSelector.Invoke(x)) { Name = nameSelector?.Invoke(x) }).ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<Tuple<string, double, Rect>> values)
    {
        string format = "{0}" + Environment.NewLine + "{1:0.0}%";
        return values.Select(x => new ScoreRectangleResultItem(x.Item3, x.Item2) { Name = x.Item1, Shape = new RectShape(x.Item3) { Title = string.Format(format, x.Item1, x.Item2 * 100) } }).ToResultPresenter();
    }
}

public static partial class ResultPresenterExtension
{
    public static IResultPresenter ToLineDataGridResultPresenter<T>(this IEnumerable<T> values, Func<T, VisionLine> valueSelector, Func<T, string> nameSelector)
    {
        return values.Select(x => new LineResultPresenterItem(valueSelector.Invoke(x).Start, valueSelector.Invoke(x).End) { Name = nameSelector?.Invoke(x) }).ToResultPresenter();
    }

    public static IResultPresenter ToCircleDataGridResultPresenter<T>(this IEnumerable<T> values, Func<T, VisionCircle> valueSelector, Func<T, string> nameSelector)
    {
        return values.Select(x => new CircleResultPresenterItem(valueSelector.Invoke(x).Point, valueSelector.Invoke(x).Radius) { Name = nameSelector?.Invoke(x) }).ToResultPresenter();
    }

    public static IResultPresenter ToPointDataGridResultPresenter<T>(this IEnumerable<T> values, Func<T, Point> valueSelector, Func<T, string> nameSelector)
    {
        return values.Select(x => new PointResultPresenterItem(valueSelector(x)) { Name = nameSelector?.Invoke(x) }).ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IFromToShape value)
    {
        return value.ToEnumerable().ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<IFromToShape> values, Action<LineResultPresenterItem> action = null)
    {
        return values.Select(x =>
        {
            string text = x is ITextable t ? t.Text : string.Empty;
            var item = new LineResultPresenterItem(x.From, x.To) { Name = text, Shape = x };
            action?.Invoke(item);
            return item;
        }).ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this VisionLine value, string name)
    {
        return value.ToEnumerable().ToLineDataGridResultPresenter(x => value, x => name);
    }

    public static IResultPresenter ToResultPresenter(this ICircleShape value)
    {
        return value.ToEnumerable().ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<ICircleShape> value)
    {
        return value.Select(x => new CircleResultPresenterItem(x.Center, x.Radius) { Name = x.Title, Shape = x }).ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<Point> value, Func<Point, string> nameSelector)
    {
        return value.ToPointDataGridResultPresenter(x => x, nameSelector);
    }
    public static IResultPresenter ToResultPresenter(this IPointShape value)
    {
        return value.ToEnumerable().ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<IPointShape> value)
    {
        return value.Select(x => new PointResultPresenterItem(x.Point) { Name = x.Title, Shape = x }).ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<IRectShape> value)
    {
        return value.Select(x => new RectangleResultPresenterItem(x.Rect) { Name = x.Title, Shape = x }).ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IRectShape value)
    {
        return value.ToEnumerable().ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<IPointsShape> value)
    {
        return value.Select(x => new PointsShapeResultPresenterItem(x) { Name = x.Title }).ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IRotatedRectShape value)
    {
        return value.ToEnumerable().ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<IRotatedRectShape> value)
    {
        return value.Select(x => new RotatedRectShapeResultPresenterItem(x) { Name = x.Title }).ToResultPresenter();
    }

    public static IResultPresenter ToResultPresenter(this IPointsShape value)
    {
        return value.ToEnumerable().ToResultPresenter();
    }

    public static IResultPresenter ToAutoResultPresenter(this IShape value, string name)
    {
        if (value is IPointShape pointShape)
            return pointShape.ToResultPresenter();
        if (value is IFromToShape lineShape)
            return lineShape.ToResultPresenter();
        if (value is ICircleShape circleShape)
            return circleShape.ToResultPresenter();
        if (value is IRectShape rectShape)
            return rectShape.ToResultPresenter();
        if (value is IRotatedRectShape rotatedRectShape)
            return rotatedRectShape.ToResultPresenter();
        if (value is IPointsShape pointsShape)
            return pointsShape.ToResultPresenter();
        Trace.Assert(false, "未找到合适的结果展示器");
        return null;
    }

    public static IResultPresenter ToAutoResultPresenter(this IEnumerable<IShape> shapes)
    {
        {
            var cshapes = shapes.OfType<ICircleShape>();
            if (cshapes.Count() > 0)
                return cshapes.ToResultPresenter();
        }
        {
            var cshapes = shapes.OfType<IRectShape>();
            if (cshapes.Count() > 0)
                return cshapes.ToResultPresenter();
        }
        {
            var cshapes = shapes.OfType<IPointsShape>();
            if (cshapes.Count() > 0)
                return cshapes.ToResultPresenter();
        }
        {
            var cshapes = shapes.OfType<IPointShape>();
            if (cshapes.Count() > 0)
                return cshapes.ToResultPresenter();
        }
        {
            var cshapes = shapes.OfType<IFromToShape>();
            if (cshapes.Count() > 0)
                return cshapes.ToResultPresenter();
        }
        {
            var cshapes = shapes.OfType<IRotatedRectShape>();
            if (cshapes.Count() > 0)
                return cshapes.ToResultPresenter();
        }
        Trace.Assert(false, "未找到合适的结果展示器");
        return null;

    }
}
