namespace H.Extensions.Unit
{
    public class MolarConcentrationUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new MolarConcentration(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return MolarConcentration.Parse(str).Value;
        }
    }
}

