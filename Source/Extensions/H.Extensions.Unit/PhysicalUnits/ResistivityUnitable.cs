namespace H.Extensions.Unit
{
    public class ResistivityUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Resistivity(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Resistivity.Parse(str).Value;
        }
    }
}

