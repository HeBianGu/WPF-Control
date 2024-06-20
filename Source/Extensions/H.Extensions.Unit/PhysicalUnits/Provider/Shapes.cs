
using System;

namespace H.Extensions.Unit
{
    #region 2-D shapes
    public struct Circle
    {
        public Length Radius { get; init; }

        public Circle(Length r)
        {
            this.Radius = r;
        }

        public Length Diameter { get { return 2 * this.Radius; } }
        public Length Circumference { get { return 2 * Math.PI * this.Radius; } }
        public Area Area { get { return Math.PI * this.Radius.Squared(); } }

        public static Circle OfRadius(Length r)
        {
            return new Circle(r);
        }

        public static Circle OfDiameter(Length d)
        {
            return new Circle(d / 2);
        }
    }

    public struct Rectangle
    {
        public Length Height { get; init; }
        public Length Width { get; init; }
        public Rectangle(Length h, Length w)
        {
            this.Height = h;
            this.Width = w;
        }

        public Area Area { get { return this.Height * this.Width; } }
    }

    #endregion

    #region Platonic 3-D shapes:

    public struct Cylinder
    {
        public Length Radius { get; set; }
        public Length Height { get; set; }
        public Cylinder(Length r, Length h)
        {
            this.Radius = r;
            this.Height = h;
        }
        public Circle EndCap { get { return Circle.OfRadius(this.Radius); } }

        public Area Area
        {
            get
            {
                Circle endCap = this.EndCap;
                Length circumference = endCap.Circumference;
                return circumference * this.Height + 2 * endCap.Area;
            }
        }

        public Volume Volume
        {
            get
            {
                Circle endCap = this.EndCap;
                return endCap.Area * this.Height;
            }
        }

    }

    public struct Sphere
    {
        public Length Radius { get; set; }

        public Sphere(Length radius)
        {
            this.Radius = radius;
        }

        public Area Area { get { return 4 * Math.PI * this.Radius.Squared(); } }

        public Volume Volume { get { return 4.0 / 3.0 * Math.PI * this.Radius.Cubed(); } }
    }
    #endregion

    #region Physical 3-D shapes with mass

    public abstract class SolidShape
    {
        public Mass Mass { get; init; }

        protected SolidShape() { }

        protected SolidShape(Mass m)
        {
            this.Mass = m;
        }

        public abstract Area SurfaceArea { get; }
        /// <summary>
        /// The external volume, ie the volume that this shape would displace.
        /// </summary>
        public abstract Volume ExternalVolume { get; }
        /// <summary>
        /// The volume of material in the shape, which we can use to calculate the density.
        /// Assumed to be the same as the external volume by default.
        /// </summary>
        public virtual Volume MaterialVolume { get { return this.ExternalVolume; } }
        public Density Density { get { return this.Mass / this.MaterialVolume; } }
        public abstract Length RadiusOfGyration { get; }
        public MomentOfInertia MomentOfInertia { get { return this.Mass * this.RadiusOfGyration.Squared(); } }
    }

    public class SolidCylinder : SolidShape
    {
        public Length Radius { get; set; }
        public Length Height { get; set; }

        public SolidCylinder()
        {
        }

        public SolidCylinder(Mass m, Length r, Length h) : base(m)
        {
            this.Radius = r;
            this.Height = h;
        }

        public override Area SurfaceArea { get { return new Cylinder(this.Radius, this.Height).Area; } }

        public override Volume ExternalVolume { get { return new Cylinder(this.Radius, this.Height).Volume; } }

        public override Length RadiusOfGyration { get { return Math.Sqrt(0.5) * this.Radius; } }
    }

    public class SolidSphere : SolidShape
    {
        public Length Radius { get; set; }

        public SolidSphere()
        {
        }

        public SolidSphere(Mass m, Length r) : base(m)
        {
            this.Radius = r;
        }

        public override Area SurfaceArea { get { return new Sphere(this.Radius).Area; } }

        public override Volume ExternalVolume { get { return new Sphere(this.Radius).Volume; } }

        public override Length RadiusOfGyration { get { return Math.Sqrt(2.0 / 5.0) * this.Radius; } }
    }

    #endregion
}