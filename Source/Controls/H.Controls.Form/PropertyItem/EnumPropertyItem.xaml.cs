// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Reflection;

namespace H.Controls.Form
{
    public class EnumPropertyItem : ObjectPropertyItem<Enum>
    {
        public EnumPropertyItem(PropertyInfo property, object obj)
            : base(property, obj)
        {
        }
    }

}
