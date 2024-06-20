// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media;

namespace H.Controls.Chart2D
{
    /// <summary>下三角Marker</summary>
    public class TrianglePointMarker : ShapePointMarker
    {

        /// <summary> 描绘形状 </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                double Size = 5;
                Point pt0 = Point.Add(this.ScreenPoint, new Vector(-Size / 2, -Size / 2));
                Point pt1 = Point.Add(this.ScreenPoint, new Vector(0, Size / 2));
                Point pt2 = Point.Add(this.ScreenPoint, new Vector(Size / 2, -Size / 2));

                StreamGeometry streamGeom = new StreamGeometry();
                using (StreamGeometryContext context = streamGeom.Open())
                {
                    context.BeginFigure(pt0, true, true);
                    context.LineTo(pt1, true, true);
                    context.LineTo(pt2, true, true);
                }
                return streamGeom;
            }
        }


        /// <summary> 绘制形状 </summary>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawGeometry(this.Fill, this.Pen, this.DefiningGeometry);
        }
    }
}
