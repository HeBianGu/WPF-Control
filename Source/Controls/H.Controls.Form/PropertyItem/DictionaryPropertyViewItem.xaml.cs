﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Reflection;

namespace H.Controls.Form
{
    public class DictionaryPropertyViewItem : ObjectPropertyItem<IDictionary>, IPropertyViewItem
    {
        public DictionaryPropertyViewItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

    }
}
