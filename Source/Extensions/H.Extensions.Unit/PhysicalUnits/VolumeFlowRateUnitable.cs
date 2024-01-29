namespace H.Extensions.Unit
{
    public class VolumeFlowRateUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new VolumeFlowRate(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return VolumeFlowRate.Parse(str).Value;
        }
    }
}

