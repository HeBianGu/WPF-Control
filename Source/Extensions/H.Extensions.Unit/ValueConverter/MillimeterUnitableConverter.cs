

namespace H.Extensions.Unit
{
    public class MillimeterUnitableConverter : UnitableValueConverterBase
    {
        protected override IUnitable GetUnitable() => new MillimeterUnitable();
    }
}

