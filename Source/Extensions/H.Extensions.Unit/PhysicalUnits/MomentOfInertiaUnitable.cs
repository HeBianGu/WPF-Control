namespace H.Extensions.Unit
{
    public class MomentOfInertiaUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new MomentOfInertia(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return MomentOfInertia.Parse(str).Value;
        }
    }
}

