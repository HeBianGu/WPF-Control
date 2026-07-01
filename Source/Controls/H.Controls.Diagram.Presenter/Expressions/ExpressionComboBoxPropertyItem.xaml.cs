// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.Form.PropertyItem.Base;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Controls.Form.PropertyItems;
using H.Controls.Form.PropertyItems.Base;
using System.Windows.Controls.Primitives;

namespace H.Controls.Diagram.Presenter.Expressions
{
    public class ExpressionComboBoxPropertyItem : ComboBoxTextPropertyItemItem
    {
        public ExpressionComboBoxPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }
}
