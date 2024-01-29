namespace H.Extensions.Unit
{
    public class AmountOfSubstanceUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new AmountOfSubstance(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return AmountOfSubstance.Parse(str).Value;
        }
    }
}

