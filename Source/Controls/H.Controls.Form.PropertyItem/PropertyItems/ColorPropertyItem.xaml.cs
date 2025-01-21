// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Reflection;
using System.Windows.Media;

namespace H.Controls.Form.PropertyItem.PropertyItems
{
    public class ColorPropertyItem : ObjectPropertyItem<Color>
    {
        public ColorPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }
}
