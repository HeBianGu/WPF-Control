namespace H.Extensions.Unit
{
    public class DimensionlessUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Dimensionless(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Dimensionless.Parse(str).Value;
        }
    }
}

