namespace H.Extensions.Unit
{
    public class AngleUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Angle(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Angle.Parse(str).Value;
        }
    }
}

