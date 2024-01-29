namespace H.Extensions.Unit
{
    public class MomentumUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Momentum(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Momentum.Parse(str).Value;
        }
    }
}

