// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItem.Attribute
{
    public class GetStandardColorsSourceAttribute : GetSourceAttribute
    {
        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            yield return Colors.Transparent;
            yield return Colors.White;
            yield return Colors.Gray;
            yield return Colors.Black;
            yield return Colors.Red;
            yield return Colors.Green;
            yield return Colors.Blue;
            yield return Colors.Yellow;
            yield return Colors.Orange;
            yield return Colors.Purple;
        }
    }
}
