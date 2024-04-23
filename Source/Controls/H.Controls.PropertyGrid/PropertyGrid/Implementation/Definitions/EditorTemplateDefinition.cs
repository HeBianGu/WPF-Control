// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Controls.PropertyGrid
{
    public class EditorTemplateDefinition : EditorDefinitionBase
    {


        #region EditingTemplate
        public static readonly DependencyProperty EditingTemplateProperty =
            DependencyProperty.Register("EditingTemplate", typeof(DataTemplate), typeof(EditorTemplateDefinition), new UIPropertyMetadata(null));

        public DataTemplate EditingTemplate
        {
            get { return (DataTemplate)GetValue(EditingTemplateProperty); }
            set { SetValue(EditingTemplateProperty, value); }
        }
        #endregion //EditingTemplate

        protected sealed override FrameworkElement GenerateEditingElement(PropertyItemBase propertyItem)
        {
            return (this.EditingTemplate != null)
              ? this.EditingTemplate.LoadContent() as FrameworkElement
              : null;
        }
    }
}
