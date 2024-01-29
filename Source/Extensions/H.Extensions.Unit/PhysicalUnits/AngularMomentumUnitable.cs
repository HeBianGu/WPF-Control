namespace H.Extensions.Unit
{
    public class AngularMomentumUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new AngularMomentum(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return AngularMomentum.Parse(str).Value;
        }
    }
}

