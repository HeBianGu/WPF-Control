namespace H.Extensions.Unit
{
    public class CurrentUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Current(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Current.Parse(str).Value;
        }
    }
}

