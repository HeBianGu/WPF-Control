// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;

namespace H.Controls.PropertyGrid
{
    public class MaskedTextBoxEditor : TypeEditor<MaskedTextBox>
    {
        public string Mask
        {
            get;
            set;
        }

        public Type ValueDataType
        {
            get;
            set;
        }

        protected override MaskedTextBox CreateEditor()
        {
            return new PropertyGridEditorMaskedTextBox();
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            this.Editor.BorderThickness = new System.Windows.Thickness(0);
            this.Editor.ValueDataType = this.ValueDataType;
            this.Editor.Mask = this.Mask;
        }

        protected override void SetValueDependencyProperty()
        {
            this.ValueProperty = MaskedTextBox.ValueProperty;
        }
    }

    public class PropertyGridEditorMaskedTextBox : MaskedTextBox
    {
        static PropertyGridEditorMaskedTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorMaskedTextBox), new FrameworkPropertyMetadata(typeof(PropertyGridEditorMaskedTextBox)));
        }
    }
}
