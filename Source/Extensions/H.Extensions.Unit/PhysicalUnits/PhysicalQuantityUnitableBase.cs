// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Unit
{
    public abstract class PhysicalQuantityUnitableBase : IUnitable
    {
        public UnitsSystem.FormatOption FormatOption { get; set; } = UnitsSystem.FormatOption.BestFit;

        public abstract double Parse(string str);

        public abstract string ToString(double value);

        object IUnitable.Parse(string str)
        {
            return Parse(str);
        }

        string IUnitable.ToString(object value)
        {
            if (value == null)
                return string.Empty;
            if (double.TryParse(value.ToString(), out double d))
                return ToString(d);
            return string.Empty;
        }
    }
}

