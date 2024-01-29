namespace H.Extensions.Unit
{
    public class CoefficientOfThermalExpansionUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new CoefficientOfThermalExpansion(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return CoefficientOfThermalExpansion.Parse(str).Value;
        }
    }
}

