
using System;

namespace H.Extensions.Unit
{
    public static class Functions
    {
        public static Length Sqrt(Area area)
        {
            double v = area.Value;
            return new Length(Math.Sqrt(v));
        }

        public static Velocity Sqrt(VelocitySquared velocitySquared)
        {
            double v = velocitySquared.Value;
            return new Velocity(Math.Sqrt(v));
        }

        public static Time Sqrt(TimeSquared timeSquared)
        {
            double v = timeSquared.Value;
            return new Time(Math.Sqrt(v));
        }

        public static AngularVelocity Sqrt(AngularVelocitySquared angluarVelocitySquared)
        {
            double v = angluarVelocitySquared.Value;
            return new AngularVelocity(Math.Sqrt(v));
        }

        public static PhysicalQuantity Sqrt(PhysicalQuantity qty)
        {
            double v = qty.Value;
            Dimensions d = qty.Dimensions.Sqrt();
            return new PhysicalQuantity(Math.Sqrt(v), d);
        }

        public static double Sin(Angle a)
        {
            return Math.Sin(a.Value);
        }

        public static double Cos(Angle a)
        {
            return Math.Cos(a.Value);
        }

        public static double Tan(Angle a)
        {
            return Math.Tan(a.Value);
        }

        public static Angle Acos(double d)
        {
            double a = Math.Acos(d);
            return new Angle(a);
        }
        public static Angle Asin(double d)
        {
            double a = Math.Asin(d);
            return new Angle(a);
        }

        public static Angle Atan2(Length l1, Length l2)
        {
            double a = Math.Atan2(l1.Value, l2.Value);
            return new Angle(a);
        }
    }
}
