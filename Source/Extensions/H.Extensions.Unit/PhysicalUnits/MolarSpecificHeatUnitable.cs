namespace H.Extensions.Unit
{
    public class MolarSpecificHeatUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new MolarSpecificHeat(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return MolarSpecificHeat.Parse(str).Value;
        }
    }
}

