// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using System.Reflection;

namespace H.Controls.Form.PropertyItems;

public class FormPropertyItem : ObjectPropertyItem<object>
{
    public FormPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }
}
