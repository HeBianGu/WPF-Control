namespace H.Extensions.Unit
{
    public class SpecificHeatUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new SpecificHeat(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return SpecificHeat.Parse(str).Value;
        }
    }
}

