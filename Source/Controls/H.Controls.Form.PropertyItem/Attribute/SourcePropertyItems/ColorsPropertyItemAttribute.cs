// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem
{
    public class ColorsPropertyItemAttribute : SourcePropertyItemBaseAttribute
    {

        public ColorsPropertyItemAttribute(Type type) : base(type)
        {

        }


        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            return typeof(Colors).GetProperties().Select(x => x.GetValue(null)).OfType<Color>();
        }
    }

    public class StandardColorsPropertyItemAttribute : SourcePropertyItemBaseAttribute
    {

        public StandardColorsPropertyItemAttribute(Type type) : base(type)
        {

        }


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


    // 荧光色
    public class FluorescentColorsPropertyItemAttribute : SourcePropertyItemBaseAttribute
    {

        public FluorescentColorsPropertyItemAttribute(Type type) : base(type)
        {

        }


        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            yield return Colors.Lime;          // #FF00FF00 - 荧光绿
            yield return Colors.Cyan;          // #FF00FFFF - 荧光青
            yield return Colors.Magenta;       // #FFFF00FF - 荧光紫红
            yield return Colors.Yellow;        // #FFFFFF00 - 荧光黄
            yield return Colors.Chartreuse;    // #FF7FFF00 - 黄绿色
        }
    }

    // 高亮
    public class HighColorsPropertyItemAttribute : SourcePropertyItemBaseAttribute
    {

        public HighColorsPropertyItemAttribute(Type type) : base(type)
        {

        }


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
