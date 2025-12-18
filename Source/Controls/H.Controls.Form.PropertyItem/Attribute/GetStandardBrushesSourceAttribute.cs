// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItem.Attribute
{
    public class GetStandardBrushesSourceAttribute : GetSourceAttribute
    {
        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            yield return Brushes.Transparent;
            yield return Brushes.White;
            yield return Brushes.Gray;
            yield return Brushes.Black;
            yield return Brushes.Red;
            yield return Brushes.Green;
            yield return Brushes.Blue;
            yield return Brushes.Yellow;
            yield return Brushes.Orange;
            yield return Brushes.Purple;
        }
    }
}
