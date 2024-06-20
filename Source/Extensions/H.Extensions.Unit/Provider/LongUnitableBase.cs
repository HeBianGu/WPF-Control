using System;

namespace H.Extensions.Unit
{
    public abstract class LongUnitableBase : UnitableBase<long>
    {
        protected override long Parse(double value, long unit)
        {
            return (long)(value * unit);
        }

        protected override long ToAbs(long value)
        {
            return Math.Abs(value);
        }

        protected override double ToRound(long value, long unit)
        {
            return (double)Math.Round(value / (decimal)unit, this.Digits);
        }
    }
}

