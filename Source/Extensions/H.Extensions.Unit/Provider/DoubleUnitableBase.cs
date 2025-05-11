// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Unit
{
    public abstract class DoubleUnitableBase : UnitableBase<double>
    {
        protected override double Parse(double value, double unit)
        {
            return value * unit;
        }

        protected override double ToAbs(double value)
        {
            return Math.Abs(value);
        }

        protected override double ToRound(double value, double unit)
        {
            return Math.Round(value / unit, this.Digits);
        }
    }
}

