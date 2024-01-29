namespace H.Extensions.Unit
{
    public class ResistanceUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Resistance(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Resistance.Parse(str).Value;
        }
    }
}

