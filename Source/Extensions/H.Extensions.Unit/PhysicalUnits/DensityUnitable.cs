namespace H.Extensions.Unit
{
    public class DensityUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Density(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Density.Parse(str).Value;
        }
    }
}

