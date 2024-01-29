namespace H.Extensions.Unit
{
    public class AbsoluteTemperatureUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new AbsoluteTemperature(value).ToString();
        }

        public override double Parse(string str)
        {
            return AbsoluteTemperature.Parse(str).Value;
        }
    }
}

