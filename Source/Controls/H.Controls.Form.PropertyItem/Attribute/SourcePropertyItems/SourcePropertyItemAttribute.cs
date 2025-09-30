// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem
{
    public class SourcePropertyItemAttribute : SourcePropertyItemBaseAttribute
    {
        public SourcePropertyItemAttribute(Type type, IEnumerable source) : base(type)
        {
            this.Source = source;
        }

        public IEnumerable Source { get; set; }

        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            return this.Source;
        }
    }
}
