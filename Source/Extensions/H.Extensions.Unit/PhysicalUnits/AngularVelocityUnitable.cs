// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Unit
{
    public class AngularVelocityUnitable : PhysicalQuantityUnitableBase
    {
        public override string ToString(double value)
        {
            return new AngularVelocity(value).ToString(this.FormatOption);
        }

        public override double Parse(string str)
        {
            return AngularVelocity.Parse(str).Value;
        }
    }
}

