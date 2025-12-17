// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem
{
    public class BrushesPropertyItemAttribute : SourcePropertyItemBaseAttribute
    {

        public BrushesPropertyItemAttribute(Type type) : base(type)
        {

        }


        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            return typeof(Brushes).GetProperties().Select(x => x.GetValue(null)).OfType<SolidColorBrush>();
        }
    }
    public class StandardBrushesPropertyItemAttribute : SourcePropertyItemBaseAttribute
    {

        public StandardBrushesPropertyItemAttribute(Type type) : base(type)
        {

        }


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

    // 荧光色
    public class FluorescentBrushesPropertyItemAttribute : SourcePropertyItemBaseAttribute
    {

        public FluorescentBrushesPropertyItemAttribute(Type type) : base(type)
        {

        }


        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            yield return Brushes.Lime;          // #FF00FF00 - 荧光绿
            yield return Brushes.Cyan;          // #FF00FFFF - 荧光青
            yield return Brushes.Magenta;       // #FFFF00FF - 荧光紫红
            yield return Brushes.Yellow;        // #FFFFFF00 - 荧光黄
            yield return Brushes.Chartreuse;    // #FF7FFF00 - 黄绿色
        }
    }

    // 高亮
    public class HighBrushesPropertyItemAttribute : SourcePropertyItemBaseAttribute
    {

        public HighBrushesPropertyItemAttribute(Type type) : base(type)
        {

        }


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
