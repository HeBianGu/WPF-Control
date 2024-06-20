using System;


namespace H.Extensions.Unit
{
    public class UnitableValueConverter : UnitableValueConverterBase
    {
        public int Digits { get; set; }
        public Type UnitableType { get; set; }
        protected override IUnitable GetUnitable()
        {
            var result = (IUnitable)Activator.CreateInstance(this.UnitableType);

            if (result is IDigits digits)
                digits.Digits = this.Digits;
            return result;
        }
    }
}

