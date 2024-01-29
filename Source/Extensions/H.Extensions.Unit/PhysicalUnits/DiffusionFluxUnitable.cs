namespace H.Extensions.Unit
{
    public class DiffusionFluxUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new DiffusionFlux(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return DiffusionFlux.Parse(str).Value;
        }
    }
}

