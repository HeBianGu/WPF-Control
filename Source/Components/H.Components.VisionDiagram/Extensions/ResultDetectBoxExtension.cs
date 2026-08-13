// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.VisionDiagram.ResultPresenters;
using H.Controls.ShapeBox.Shapes;
using H.Controls.ShapeBox.Shapes.Base;

namespace H.Components.VisionDiagram.Extensions;
public static partial class ResultDetectBoxExtension
{
    public static IEnumerable<ResultDetectBox> GetDetectBoxLabels(this IEnumerable<DefectBox> defectBoxes, List<string> classNames)
    {
        foreach (DefectBox nmsbox in defectBoxes)
        {
            string name = classNames.Count > nmsbox.ClassId ? classNames[nmsbox.ClassId] : classNames.Count == 1 ? classNames[0] : string.Empty;
            double score = nmsbox.Score;

            yield return new ResultDetectBox() { Box = nmsbox, ClassName = name, Confidence = score };
        }
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<ResultDetectBox> tuples)
    {
        //tuples = tuples.Where(x => x.Item1.Box.ToCVRect().Width > 0 && x.Item1.Box.ToCVRect().Height > 0);
        return tuples.Select(x => new ScoreRectangleResultItem(x.Box.Box, Math.Round(x.Confidence, 2)) { Name = x.ClassName }).ToResultPresenter();

        //return tuples.ToRectangleDataGridResultPresenter(x => x.Item1.Box.ToCVRect().ToWindowRect(), x => x.Item2);
    }

    public static string ToTitle(this ResultDetectBox x)
    {
        return string.Format("{0} {1:0.0}%", x.ClassName, x.Confidence * 100);
    }

    public static IEnumerable<IShape> ToRectShapes(this IEnumerable<ResultDetectBox> tuples, Action<IRectShape> action = null)
    {
        return tuples.Select(x => x.ToRectShape(action)).OfType<IShape>().ToObservable();
    }

    public static IRectShape ToRectShape(this ResultDetectBox x, Action<IRectShape> action = null)
    {
        var r = new RectShape(x.Box.Box) { Title = x.ToTitle() };
        action?.Invoke(r);
        return r;
    }

}
