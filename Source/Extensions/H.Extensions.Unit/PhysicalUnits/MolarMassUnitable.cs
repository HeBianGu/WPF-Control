namespace H.Extensions.Unit
{
    public class MolarMassUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new MolarMass(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return MolarMass.Parse(str).Value;
        }
    }
}

