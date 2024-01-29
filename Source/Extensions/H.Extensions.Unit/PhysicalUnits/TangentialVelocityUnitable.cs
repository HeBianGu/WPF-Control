namespace H.Extensions.Unit
{
    public class TangentialVelocityUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new TangentialVelocity(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return TangentialVelocity.Parse(str).Value;
        }
    }
}

