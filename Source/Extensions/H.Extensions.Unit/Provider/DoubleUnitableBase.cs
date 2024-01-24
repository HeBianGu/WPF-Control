﻿using System;

namespace H.Extensions.Unit
{
    public abstract class DoubleUnitableBase : UnitableBase<double>
    {
        protected override double ToValue(double value, double unit)
        {
            return value * unit;
        }

        protected override double ToAbs(double value)
        {
            return Math.Abs(value);
        }

        protected override double ToRound(double value, double unit)
        {
            return Math.Round(value / unit, Digits);
        }
    }
}
