using System.Windows;
using System.Windows.Media;

namespace H.Controls.OutlookBar
{
    public static class PopupHelper
    {
        public static bool IsLogicalAncestorOf(this UIElement ancestor, UIElement child)
        {
            if (child != null)
            {
                FrameworkElement obj = child as FrameworkElement;
                while (obj != null)
                {
                    FrameworkElement parent = VisualTreeHelper.GetParent(obj) as FrameworkElement;
                    obj = parent == null ? obj.Parent as FrameworkElement : parent;
                    if (obj == ancestor) return true;
                }
            }
            return false;
        }
    }
}
