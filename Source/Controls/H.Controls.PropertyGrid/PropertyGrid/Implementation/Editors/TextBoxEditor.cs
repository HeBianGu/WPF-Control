// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows.Controls;
using System.Windows;
#if !VS2008
using System.ComponentModel.DataAnnotations;

#endif

namespace H.Controls.PropertyGrid
{
    public class TextBoxEditor : TypeEditor<WatermarkTextBox>
    {
        protected override WatermarkTextBox CreateEditor()
        {
            return new PropertyGridEditorTextBox();
        }

#if !VS2008
        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            DisplayAttribute displayAttribute = PropertyGridUtilities.GetAttribute<DisplayAttribute>(propertyItem.PropertyDescriptor);
            if (displayAttribute != null)
            {
                this.Editor.Watermark = displayAttribute.GetPrompt();
            }
        }
#endif

        protected override void SetValueDependencyProperty()
        {
            this.ValueProperty = TextBox.TextProperty;
        }
    }

    public class PropertyGridEditorTextBox : WatermarkTextBox
    {
        static PropertyGridEditorTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorTextBox), new FrameworkPropertyMetadata(typeof(PropertyGridEditorTextBox)));
        }
    }
}
