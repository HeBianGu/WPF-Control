namespace H.Extensions.Unit
{
    public class TemperatureChangeUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new TemperatureChange(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return TemperatureChange.Parse(str).Value;
        }
    }
}

