
using System;
using System.Windows;
using System.Windows.Media;

namespace H.Controls.ZoomBox
{
    public static class VisualTreeHelperEx
    {
        public static DependencyObject FindAncestorByType(DependencyObject element, Type type, bool specificTypeOnly)
        {
            if (element == null)
                return null;

            if (specificTypeOnly ? element.GetType() == type
                : element.GetType() == type || element.GetType().IsSubclassOf(type))
                return element;

            return FindAncestorByType(VisualTreeHelper.GetParent(element), type, specificTypeOnly);
        }

        public static T FindAncestorByType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null)
            {
                return default;
            }
            if (depObj is T)
            {
                return (T)depObj;
            }

            T parent = default;

            parent = FindAncestorByType<T>(VisualTreeHelper.GetParent(depObj));

            return parent;
        }

        public static Visual FindDescendantByName(Visual element, string name)
        {
            if (element != null && element is FrameworkElement && (element as FrameworkElement).Name == name)
                return element;

            Visual foundElement = null;
            if (element is FrameworkElement)
                (element as FrameworkElement).ApplyTemplate();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = FindDescendantByName(visual, name);
                if (foundElement != null)
                    break;
            }

            return foundElement;
        }

        public static Visual FindDescendantByType(Visual element, Type type)
        {
            return FindDescendantByType(element, type, true);
        }

        public static Visual FindDescendantByType(Visual element, Type type, bool specificTypeOnly)
        {
            if (element == null)
                return null;

            if (specificTypeOnly ? element.GetType() == type
                : element.GetType() == type || element.GetType().IsSubclassOf(type))
                return element;

            Visual foundElement = null;
            if (element is FrameworkElement)
                (element as FrameworkElement).ApplyTemplate();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = FindDescendantByType(visual, type, specificTypeOnly);
                if (foundElement != null)
                    break;
            }

            return foundElement;
        }

        public static T FindDescendantByType<T>(Visual element) where T : Visual
        {
            Visual temp = FindDescendantByType(element, typeof(T));

            return (T)temp;
        }

        public static Visual FindDescendantWithPropertyValue(Visual element,
            DependencyProperty dp, object value)
        {
            if (element == null)
                return null;

            if (element.GetValue(dp).Equals(value))
                return element;

            Visual foundElement = null;
            if (element is FrameworkElement)
                (element as FrameworkElement).ApplyTemplate();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = FindDescendantWithPropertyValue(visual, dp, value);
                if (foundElement != null)
                    break;
            }

            return foundElement;
        }
    }
}
