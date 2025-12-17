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
}
