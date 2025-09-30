// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Unit
{
    public partial struct Dimensionless : IPhysicalQuantity
    {
        static public implicit operator Dimensionless(int value)
        {
            return new Dimensionless(value);
        }

        static public implicit operator Dimensionless(double value)
        {
            return new Dimensionless(value);
        }

        public static implicit operator double(Dimensionless v)
        {
            return v.Value;
        }
    }
}
