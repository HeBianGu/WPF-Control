// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Controls.Form.PropertyItems.Base;
using System.Collections.Generic;
using System.Reflection;

namespace H.Controls.Form.PropertyItem.EnumerablePropertyItems
{
    public class EnumerablePropertyItem : ObjectPropertyItem<IEnumerable<object>>
    {
        public EnumerablePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }
    }
}
