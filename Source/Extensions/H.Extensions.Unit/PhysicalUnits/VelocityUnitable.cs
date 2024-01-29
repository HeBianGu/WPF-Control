namespace H.Extensions.Unit
{
    public class VelocityUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Velocity(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Velocity.Parse(str).Value;
        }
    }
}

