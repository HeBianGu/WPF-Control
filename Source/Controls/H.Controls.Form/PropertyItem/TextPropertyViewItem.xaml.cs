﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Reflection;

namespace H.Controls.Form
{
    public class TextPropertyViewItem : TextPropertyItem, IPropertyViewItem
    {
        public TextPropertyViewItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }
    }
}
