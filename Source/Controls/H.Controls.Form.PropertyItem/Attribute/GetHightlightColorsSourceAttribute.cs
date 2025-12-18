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
    public class GetHightlightColorsSourceAttribute : GetSourceAttribute
    {
        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            // 透明和中性色
            yield return Colors.White;
            yield return Colors.Black;

            // 红色系高亮
            yield return Colors.Red;
            yield return Colors.OrangeRed;
            yield return Colors.Tomato;
            yield return Colors.Coral;
            yield return Colors.HotPink;

            // 橙色/黄色系高亮
            yield return Colors.Orange;
            yield return Colors.Gold;
            yield return Colors.Yellow;
            yield return Colors.Goldenrod;
            yield return Colors.Khaki;

            // 绿色系高亮
            yield return Colors.Lime;
            yield return Colors.Chartreuse;
            yield return Colors.LawnGreen;
            yield return Colors.GreenYellow;
            yield return Colors.SpringGreen;
            yield return Colors.MediumSpringGreen;

            // 蓝色/青色系高亮
            yield return Colors.Cyan;
            yield return Colors.Aqua;
            yield return Colors.DodgerBlue;
            yield return Colors.DeepSkyBlue;
            yield return Colors.SkyBlue;
            yield return Colors.LightSkyBlue;

            // 紫色/粉色系高亮
            yield return Colors.Magenta;
            yield return Colors.Fuchsia;
            yield return Colors.Orchid;
            yield return Colors.Violet;
            yield return Colors.MediumPurple;
        }
    }
}
