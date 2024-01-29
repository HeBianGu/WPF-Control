namespace H.Extensions.Unit
{
    public class ThermalCapacityUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new ThermalCapacity(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return ThermalCapacity.Parse(str).Value;
        }
    }
}

