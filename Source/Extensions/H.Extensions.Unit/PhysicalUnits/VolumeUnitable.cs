namespace H.Extensions.Unit
{
    public class VolumeUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new Volume(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return Volume.Parse(str).Value;
        }
    }
}

