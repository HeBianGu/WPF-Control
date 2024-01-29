namespace H.Extensions.Unit
{
    public class LuminousIntensityUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new LuminousIntensity(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return LuminousIntensity.Parse(str).Value;
        }
    }
}

