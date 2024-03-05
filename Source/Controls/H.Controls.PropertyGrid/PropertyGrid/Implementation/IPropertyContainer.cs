// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.ComponentModel;
using System.Windows;

namespace H.Controls.PropertyGrid
{
    internal interface IPropertyContainer
    {
        ContainerHelperBase ContainerHelper { get; }
        Style PropertyContainerStyle { get; }
        EditorDefinitionCollection EditorDefinitions { get; }
        PropertyDefinitionCollection PropertyDefinitions { get; }
        bool IsCategorized { get; }
        bool IsSortedAlphabetically { get; }
        bool AutoGenerateProperties { get; }
        bool HideInheritedProperties { get; }
        FilterInfo FilterInfo { get; }
        bool? IsPropertyVisible(PropertyDescriptor pd);
    }
}
