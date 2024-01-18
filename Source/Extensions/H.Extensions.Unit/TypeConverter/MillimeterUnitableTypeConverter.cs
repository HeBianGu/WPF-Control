

namespace H.Extensions.Unit
{
    public class MillimeterUnitableTypeConverter : UnitableTypeConverterBase
    {
        protected override IUnitable GetUnitable() => new ByteSizeUnitable();
    }
}

