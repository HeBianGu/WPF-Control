// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItem.Attribute
{
    // 高亮
    public class GetHightlightBrushesSourceAttribute : GetSourceAttribute
    {
        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            // 透明和中性色
            yield return Brushes.White;
            yield return Brushes.Black;

            // 红色系高亮
            yield return Brushes.Red;
            yield return Brushes.OrangeRed;
            yield return Brushes.Tomato;
            yield return Brushes.Coral;
            yield return Brushes.HotPink;

            // 橙色/黄色系高亮
            yield return Brushes.Orange;
            yield return Brushes.Gold;
            yield return Brushes.Yellow;
            yield return Brushes.Goldenrod;
            yield return Brushes.Khaki;

            // 绿色系高亮
            yield return Brushes.Lime;
            yield return Brushes.Chartreuse;
            yield return Brushes.LawnGreen;
            yield return Brushes.GreenYellow;
            yield return Brushes.SpringGreen;
            yield return Brushes.MediumSpringGreen;

            // 蓝色/青色系高亮
            yield return Brushes.Cyan;
            yield return Brushes.Aqua;
            yield return Brushes.DodgerBlue;
            yield return Brushes.DeepSkyBlue;
            yield return Brushes.SkyBlue;
            yield return Brushes.LightSkyBlue;

            // 紫色/粉色系高亮
            yield return Brushes.Magenta;
            yield return Brushes.Fuchsia;
            yield return Brushes.Orchid;
            yield return Brushes.Violet;
            yield return Brushes.MediumPurple;
        }
    }
}
