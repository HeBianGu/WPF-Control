namespace H.Extensions.Unit
{
    public class AreaUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Area(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Area.Parse(str).Value;
        }
    }
}

