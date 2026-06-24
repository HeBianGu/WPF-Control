// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Controls.Form.PropertyItem.Base;
using H.Extensions.FontIcon;
using H.Services.Message;

namespace H.Controls.Form.PropertyItem.ComboBoxPropertyItems
{
    public class ComboBoxPropertyItem : SelectSourcePropertyItem<object>, IHitTestPropertyViewItem
    {
        public ComboBoxPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }
    }

    public class RefreshComboBoxPropertyItem : ComboBoxPropertyItem
    {
        public RefreshComboBoxPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }

        [Icon(FontIcons.Sync)]
        [Display(Name = "刷新数据源", GroupName = "操作")]
        public DisplayCommand RefreshSourceCommand => new DisplayCommand(x =>
        {
            this.RefreshSource();
            IocMessage.Snack.ShowSuccess("刷新数据成功");
        });
    }

    public class NameKey
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.ID, this.Name);
        }

        public override bool Equals(object obj)
        {
            if (obj is NameKey infoWrapper)
                return this.ID.Equals(infoWrapper.ID) && this.Name.Equals(infoWrapper.Name);
            return false;
        }

        public override string ToString()
        {
            return $"{this.Name}";
        }
    }

}
