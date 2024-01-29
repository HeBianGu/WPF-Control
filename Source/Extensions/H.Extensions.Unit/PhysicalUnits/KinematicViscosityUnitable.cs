namespace H.Extensions.Unit
{
    public class KinematicViscosityUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new KinematicViscosity(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return KinematicViscosity.Parse(str).Value;
        }
    }
}

