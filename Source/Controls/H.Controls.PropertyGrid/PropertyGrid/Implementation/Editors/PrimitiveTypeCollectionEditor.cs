// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Controls.PropertyGrid
{
    public class PrimitiveTypeCollectionEditor : TypeEditor<PrimitiveTypeCollectionControl>
    {
        protected override void SetControlProperties(PropertyItem propertyItem)
        {
            this.Editor.BorderThickness = new System.Windows.Thickness(0);
            this.Editor.Content = "(Collection)";
        }

        protected override void SetValueDependencyProperty()
        {
            this.ValueProperty = PrimitiveTypeCollectionControl.ItemsSourceProperty;
        }

        protected override PrimitiveTypeCollectionControl CreateEditor()
        {
            return new PropertyGridEditorPrimitiveTypeCollectionControl();
        }

        protected override void ResolveValueBinding(PropertyItem propertyItem)
        {
            System.Type type = propertyItem.PropertyType;
            this.Editor.ItemsSourceType = type;

            if (type.BaseType == typeof(System.Array))
            {
                this.Editor.ItemType = type.GetElementType();
            }
            else
            {
                System.Type[] typeArguments = type.GetGenericArguments();
                if (typeArguments.Length > 0)
                {
                    this.Editor.ItemType = typeArguments[0];
                }
            }

            base.ResolveValueBinding(propertyItem);
        }
    }

    public class PropertyGridEditorPrimitiveTypeCollectionControl : PrimitiveTypeCollectionControl
    {
        static PropertyGridEditorPrimitiveTypeCollectionControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyGridEditorPrimitiveTypeCollectionControl), new FrameworkPropertyMetadata(typeof(PropertyGridEditorPrimitiveTypeCollectionControl)));
        }
    }
}
