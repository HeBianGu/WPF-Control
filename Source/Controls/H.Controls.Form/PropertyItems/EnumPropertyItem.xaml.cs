// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Controls.Form.PropertyItems.Base;

namespace H.Controls.Form.PropertyItems;

public class EnumPropertyItem : ObjectPropertyItem<Enum>
{
    public EnumPropertyItem(PropertyInfo property, object obj)
        : base(property, obj)
    {
    }
}
