// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Controls.PropertyGrid
{
    public interface ITypeEditor
    {
        FrameworkElement ResolveEditor(PropertyItem propertyItem);
    }
}
