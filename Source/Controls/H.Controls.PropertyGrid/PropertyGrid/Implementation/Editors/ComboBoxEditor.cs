// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Windows;

namespace H.Controls.PropertyGrid
{
    public abstract class ComboBoxEditor : TypeEditor<System.Windows.Controls.ComboBox>
    {
        protected override void SetValueDependencyProperty()
        {
            this.ValueProperty = System.Windows.Controls.ComboBox.SelectedItemProperty;
        }

        protected override System.Windows.Controls.ComboBox CreateEditor()
        {
            return new PropertyGridEditorComboBox();
        }

        protected override void ResolveValueBinding(PropertyItem propertyItem)
        {
            SetItemsSource(propertyItem);
            base.ResolveValueBinding(propertyItem);
        }

        protected abstract IEnumerable CreateItemsSource(PropertyItem propertyItem);

        private void SetItemsSource(PropertyItem propertyItem)
        {
            this.Editor.ItemsSource = CreateItemsSource(propertyItem);
        }
    }

    public class PropertyGridEditorComboBox : System.Windows.Controls.ComboBox
    {
        static PropertyGridEditorComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorComboBox), new FrameworkPropertyMetadata(typeof(PropertyGridEditorComboBox)));
        }
    }
}
