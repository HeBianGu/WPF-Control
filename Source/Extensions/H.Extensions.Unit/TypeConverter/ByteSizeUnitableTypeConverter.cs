

namespace H.Extensions.Unit
{
    public class ByteSizeUnitableTypeConverter : UnitableTypeConverterBase
    {
        protected override IUnitable GetUnitable() => new ByteSizeUnitable();
    }
}

