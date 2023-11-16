// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows.Media;

namespace H.Controls.Diagram
{
    public abstract class FigureBase : ICloneable
    {
        public FigureBase()
        {
            this.Name = this.GetType().Name;
        }
        public string Name { get; set; }

        public double Width { get; set; } = 100;

        public double Height { get; set; } = 100;

        public Brush Background { get; set; } = Brushes.Gray;

        public Brush Foreground { get; set; } = Brushes.White;

        public object Clone()
        {
            object n = Activator.CreateInstance(this.GetType());

            System.Reflection.PropertyInfo[] ps = this.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo p in ps)
            {
                p.SetValue(n, p.GetValue(this));
            }

            return n;
        }
    }
    /// <summary> 正方形 </summary>
    public class Square : FigureBase
    {
    }

    /// <summary>
    /// 菱形
    /// </summary>
    public class Diamond : FigureBase
    {
    }

    /// <summary>
    /// 三角形
    /// </summary>
    public class Trangle : FigureBase
    {

    }

    /// <summary>
    /// 圆形
    /// </summary>
    public class Circle : FigureBase
    {

    }
}
