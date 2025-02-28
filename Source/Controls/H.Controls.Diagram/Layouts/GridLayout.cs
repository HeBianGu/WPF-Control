// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Extensions.TypeConverter;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace H.Controls.Diagram.Layouts;

/// <summary> 网格布局 </summary>
[DisplayName("GridLayout")]
[TypeConverter(typeof(DisplayNameConverter))]
public class GridLayout : Layout
{
    public override void DoLayout(params Node[] nodes)
    {
        double height = this.Diagram.ActualHeight;

        List<Node> list = nodes.ToList();

        //double space = 50.0;

        //double x = 0.0;

        for (int i = 0; i < list.Count(); i++)
        {
            Node item = list[i];

            //if (item.VisualElement == null) continue;

            ////item.ApplyTemplate();

            //var size = item.GetEffectiveSize(item.VisualElement);

            //item.Bounds = new Rect(new Point(x, height / 2), size); 

            //x += size.Width + space;
        }
    }
}
