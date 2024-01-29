namespace H.Extensions.Unit
{
    public class CoefficientOfViscosityUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new CoefficientOfViscosity(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return CoefficientOfViscosity.Parse(str).Value;
        }
    }
}

