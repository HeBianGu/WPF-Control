namespace H.Extensions.Unit
{
    public class ElectricPotentialUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new ElectricPotential(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return ElectricPotential.Parse(str).Value;
        }
    }
}

