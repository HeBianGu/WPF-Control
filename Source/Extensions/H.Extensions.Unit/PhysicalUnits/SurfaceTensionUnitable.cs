namespace H.Extensions.Unit
{
    public class SurfaceTensionUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new SurfaceTension(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return SurfaceTension.Parse(str).Value;
        }
    }
}

