namespace H.Extensions.Unit
{
    public class IlluminanceUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Illuminance(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Illuminance.Parse(str).Value;
        }
    }
}

