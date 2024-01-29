namespace H.Extensions.Unit
{
    public class LuminousFluxUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new LuminousFlux(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return LuminousFlux.Parse(str).Value;
        }
    }
}

