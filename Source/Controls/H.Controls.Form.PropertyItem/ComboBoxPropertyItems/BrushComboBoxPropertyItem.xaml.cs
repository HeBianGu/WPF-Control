// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Base;

namespace H.Controls.Form.PropertyItem.ComboBoxPropertyItems
{
    public class BrushComboBoxPropertyItem : SelectSourcePropertyItem<SolidColorBrush>
    {
        public BrushComboBoxPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        protected override IEnumerable<SolidColorBrush> CreateSource()
        {
            var source = base.CreateSource();
            var all = source ?? typeof(Brushes).GetProperties().Select(x => x.GetValue(null)).OfType<SolidColorBrush>();
            if (!all.Contains(this.Value))
                yield return this.Value;
            foreach (var item in all)
                yield return item;
        }

        protected override void SetValue(SolidColorBrush value)
        {
            this.PropertyInfo.SetValue(this.Obj, value);
        }
    }
}
