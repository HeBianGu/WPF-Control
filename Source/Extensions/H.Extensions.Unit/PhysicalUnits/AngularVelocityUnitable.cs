namespace H.Extensions.Unit
{
    public class AngularVelocityUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new AngularVelocity(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return AngularVelocity.Parse(str).Value;
        }
    }
}

