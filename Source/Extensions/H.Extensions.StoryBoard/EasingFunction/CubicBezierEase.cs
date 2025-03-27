// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media.Animation;

namespace H.Extensions.StoryBoard.EasingFunction;

/// <summary> 贝塞尔曲线 </summary>
public class CubicBezierEase : IEasingFunction
{
    private Point _p1;
    private Point _p2;
    private Point _c1;
    private Point _c2;

    public CubicBezierEase(Point p1, Point c1, Point c2, Point p2)
    {
        _p1 = p1;
        _c1 = c1;
        _c2 = c2;
        _p2 = p2;
    }

    public double Ease(double normalizedTime)
    {
        double t = normalizedTime;
        double invt = 1.0 - normalizedTime;
        Point result = new Point();
        result = result.Add(_p1.Multiply(invt * invt * invt));
        result = result.Add(_c1.Multiply(3 * invt * invt * t));
        result = result.Add(_c2.Multiply(3 * invt * t * t));
        result = result.Add(_p2.Multiply(t * t * t));
        return result.Y;
    }
}
