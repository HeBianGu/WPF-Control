using System;

namespace H.Extensions.Unit
{
    public abstract class IntUnitableBase : UnitableBase<int>
    {
        protected override int Parse(double value, int unit)
        {
            return (int)(value * unit);
        }

        protected override int ToAbs(int value)
        {
            return Math.Abs(value);
        }

        protected override double ToRound(int value, int unit)
        {
            return (double)Math.Round(value / (decimal)unit, this.Digits);
        }
    }
}

