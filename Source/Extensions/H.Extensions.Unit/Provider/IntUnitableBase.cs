// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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

