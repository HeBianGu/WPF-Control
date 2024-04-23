// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Controls.PropertyGrid
{
    public class ColorEditor : TypeEditor<ColorPicker>
    {
        protected override ColorPicker CreateEditor()
        {
            return new PropertyGridEditorColorPicker();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            Editor.BorderThickness = new System.Windows.Thickness(0);
            Editor.DisplayColorAndName = true;
        }
        protected override void SetValueDependencyProperty()
        {
            ValueProperty = ColorPicker.SelectedColorProperty;
        }
    }

    public class PropertyGridEditorColorPicker : ColorPicker
    {
        static PropertyGridEditorColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorColorPicker), new FrameworkPropertyMetadata(typeof(PropertyGridEditorColorPicker)));
        }
    }
}
