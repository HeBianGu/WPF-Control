// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls;

namespace H.Controls.PropertyGrid
{
    public class TextBlockEditor : TypeEditor<TextBlock>
    {
        protected override TextBlock CreateEditor()
        {
            return new PropertyGridEditorTextBlock();
        }

        protected override void SetValueDependencyProperty()
        {
            this.ValueProperty = TextBlock.TextProperty;
        }

        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            this.Editor.Margin = new System.Windows.Thickness(5, 0, 0, 0);
            this.Editor.TextTrimming = TextTrimming.CharacterEllipsis;
        }
    }

    public class PropertyGridEditorTextBlock : TextBlock
    {
        static PropertyGridEditorTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorTextBlock), new FrameworkPropertyMetadata(typeof(PropertyGridEditorTextBlock)));
        }
    }
}
