namespace H.Extensions.Unit
{
    public class AccelerationUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Acceleration(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Acceleration.Parse(str).Value;
        }
    }
}

