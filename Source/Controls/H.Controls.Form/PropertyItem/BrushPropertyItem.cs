// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Reflection;
using System.Windows.Media;

namespace H.Controls.Form
{
    public class BrushPropertyItem : ObjectPropertyItem<SolidColorBrush>
    {
        public BrushPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }
}
