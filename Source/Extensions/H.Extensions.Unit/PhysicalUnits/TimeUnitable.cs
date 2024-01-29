namespace H.Extensions.Unit
{
    public class TimeUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Time(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Time.Parse(str).Value;
        }
    }
}

