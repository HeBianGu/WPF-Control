// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls;

namespace H.Controls.PropertyGrid
{
    public class CheckBoxEditor : TypeEditor<CheckBox>
    {
        protected override CheckBox CreateEditor()
        {
            return new PropertyGridEditorCheckBox();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            this.Editor.Margin = new Thickness(5, 0, 0, 0);
        }

        protected override void SetValueDependencyProperty()
        {
            this.ValueProperty = CheckBox.IsCheckedProperty;
        }
    }

    public class PropertyGridEditorCheckBox : CheckBox
    {
        static PropertyGridEditorCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorCheckBox), new FrameworkPropertyMetadata(typeof(PropertyGridEditorCheckBox)));
        }
    }
}
