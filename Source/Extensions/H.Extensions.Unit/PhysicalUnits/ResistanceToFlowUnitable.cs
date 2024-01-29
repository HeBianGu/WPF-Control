namespace H.Extensions.Unit
{
    public class ResistanceToFlowUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new ResistanceToFlow(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return ResistanceToFlow.Parse(str).Value;
        }
    }
}

