namespace H.Extensions.Unit
{
    public class PressureUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Pressure(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Pressure.Parse(str).Value;
        }
    }
}

