// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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

