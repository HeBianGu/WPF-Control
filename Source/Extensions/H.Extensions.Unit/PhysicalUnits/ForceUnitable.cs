namespace H.Extensions.Unit
{
    public class ForceUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Force(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Force.Parse(str).Value;
        }
    }
}

