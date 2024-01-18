

namespace H.Extensions.Unit
{
    public class ByteSizeUnitableConverter : UnitableValueConverterBase
    {
        protected override IUnitable GetUnitable() => new ByteSizeUnitable();
    }
}

