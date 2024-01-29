namespace H.Extensions.Unit
{
    public class PowerUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Power(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Power.Parse(str).Value;
        }
    }
}

