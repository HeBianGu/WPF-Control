namespace H.Extensions.Unit
{
    public class SolidAngleUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new SolidAngle(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return SolidAngle.Parse(str).Value;
        }
    }
}

