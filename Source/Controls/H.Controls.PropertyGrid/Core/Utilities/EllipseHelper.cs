// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;

namespace H.Controls.PropertyGrid
{
    internal static class EllipseHelper
    {
        public static Point PointOfRadialIntersection(Rect ellipseRect, double angle)
        {
            // given by the formula:
            //
            // x =  a cos q,
            // y =  b sin q,
            //
            // where a is the elliptical radius along the major axis
            //       b is the elliptical radius along the minor axis
            //       q is the central angle from the major axis

            double a = ellipseRect.Width / 2;
            double b = ellipseRect.Height / 2;

            // since this is WPF, we can assume angle is currently specified in degrees, so convert to radians
            double q = angle * Math.PI / 180;

            return RectHelper.Center(ellipseRect) + new Vector(a * Math.Cos(q), b * Math.Sin(q));
        }

        public static double RadialDistanceFromCenter(Rect ellipseRect, double angle)
        {
            // given by the formula:
            //
            //              2 2
            //   2         a b
            //  r  = -----------------
            //        2   2     2   2
            //       a sin q + b cos q
            //
            // where a is the elliptical radius along the major axis
            //       b is the elliptical radius along the minor axis
            //       q is the central angle from the major axis

            double a = ellipseRect.Width / 2;
            double b = ellipseRect.Height / 2;

            // since this is WPF, we can assume angle is currently specified in degrees, so convert to radians
            double q = angle * Math.PI / 180;

            double sinq = Math.Sin(q);
            double cosq = Math.Cos(q);
            return Math.Sqrt(a * a * b * b / ((a * a * sinq * sinq) + (b * b * cosq * cosq)));
        }
    }
}
