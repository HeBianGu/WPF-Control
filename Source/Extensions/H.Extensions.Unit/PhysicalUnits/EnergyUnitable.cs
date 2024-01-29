namespace H.Extensions.Unit
{
    public class EnergyUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Energy(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Energy.Parse(str).Value;
        }
    }
}

