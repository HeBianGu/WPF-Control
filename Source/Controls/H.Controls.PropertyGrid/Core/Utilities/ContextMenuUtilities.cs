// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Controls.PropertyGrid
{
    public class ContextMenuUtilities
    {
        public static readonly DependencyProperty OpenOnMouseLeftButtonClickProperty = DependencyProperty.RegisterAttached("OpenOnMouseLeftButtonClick", typeof(bool), typeof(ContextMenuUtilities), new FrameworkPropertyMetadata(false, OpenOnMouseLeftButtonClickChanged));
        public static void SetOpenOnMouseLeftButtonClick(FrameworkElement element, bool value)
        {
            element.SetValue(OpenOnMouseLeftButtonClickProperty, value);
        }
        public static bool GetOpenOnMouseLeftButtonClick(FrameworkElement element)
        {
            return (bool)element.GetValue(OpenOnMouseLeftButtonClickProperty);
        }

        public static void OpenOnMouseLeftButtonClickChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement control = (FrameworkElement)sender;
            if ((bool)e.NewValue)
            {
                control.PreviewMouseLeftButtonDown += (s, args) =>
                {
                    if (control.ContextMenu != null)
                    {
                        control.ContextMenu.PlacementTarget = control;
                        control.ContextMenu.IsOpen = true;
                    }
                };
            }
            //TODO: remove handler when set to false
        }
    }
}
