namespace H.Extensions.Unit
{
    public class MassUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Mass(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Mass.Parse(str).Value;
        }
    }
}

