namespace H.Extensions.Unit
{
    public class LengthUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Length(value).ToString();
        }

        public override double Parse(string str)
        {
            return Length.Parse(str).Value;
        }
    }
}

