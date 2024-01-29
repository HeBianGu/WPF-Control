namespace H.Extensions.Unit
{
    public class FrequencyUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Frequency(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Frequency.Parse(str).Value;
        }
    }
}

