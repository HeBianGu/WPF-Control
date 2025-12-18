// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItem.Attribute
{
    // 荧光色
    public class GetFluorescentColorsSourceAttribute : GetSourceAttribute
    {
        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            yield return Colors.Lime;          // #FF00FF00 - 荧光绿
            yield return Colors.Cyan;          // #FF00FFFF - 荧光青
            yield return Colors.Magenta;       // #FFFF00FF - 荧光紫红
            yield return Colors.Yellow;        // #FFFFFF00 - 荧光黄
            yield return Colors.Chartreuse;    // #FF7FFF00 - 黄绿色
        }
    }
}
