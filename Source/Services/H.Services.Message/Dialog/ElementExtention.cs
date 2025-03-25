// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows.Controls.Primitives;

namespace H.Services.Message.Dialog;

internal static class ElementExtention
{
    public static T GetChild<T>(this DependencyObject p_element, Func<T, bool> p_func = null) where T : UIElement
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(p_element); i++)
        {
            UIElement child = VisualTreeHelper.GetChild(p_element, i) as FrameworkElement;

            if (child == null)
                continue;

            T t = child as T;

            if (t != null && (p_func == null || p_func(t)))
                return (T)child;

            T grandChild = child.GetChild(p_func);
            if (grandChild != null)
                return grandChild;
        }

        return null;
    }

    public static IEnumerable<T> GetChildren<T>(this DependencyObject p_element, Func<T, bool> p_func = null, bool filterContain = false) where T : UIElement
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(p_element); i++)
        {
            UIElement child = VisualTreeHelper.GetChild(p_element, i) as FrameworkElement;

            if (child == null)
                continue;

            if (child is T)
            {
                T t = (T)child;

                foreach (T c in child.GetChildren(p_func, filterContain))
                {
                    yield return c;
                }

                if (p_func != null && !p_func(t))
                {
                    if (filterContain)
                        yield return t;
                    continue;
                }

                yield return t;
            }

            else
            {
                foreach (T c in child.GetChildren(p_func, filterContain))
                {
                    yield return c;
                }
            }
        }
    }

    public static T GetParent<T>(this DependencyObject element) where T : DependencyObject
    {
        if (element == null) return null;
        DependencyObject parent = VisualTreeHelper.GetParent(element);
        while (parent != null && !(parent is T))
        {
            DependencyObject newVisualParent = VisualTreeHelper.GetParent(parent);
            if (newVisualParent != null)
                parent = newVisualParent;
            else
            {
                if (parent is FrameworkElement)
                    parent = (parent as FrameworkElement).Parent;
                else
                {
                    break;
                }
            }
        }
        return parent as T;
    }

    public static T GetParent<T>(this DependencyObject element, Func<T, bool> p_func) where T : DependencyObject
    {
        if (element == null) return null;
        DependencyObject parent = VisualTreeHelper.GetParent(element);
        while (parent != null && !(parent is T) || !p_func(parent as T))
        {
            if (parent == null) break;
            DependencyObject newVisualParent = VisualTreeHelper.GetParent(parent);
            if (newVisualParent != null)
                parent = newVisualParent;
            else
            {
                if (parent is FrameworkElement)
                    parent = (parent as FrameworkElement).Parent;
                else
                {
                    break;
                }
            }
        }
        return parent as T;
    }

    public static T GetElement<T>(this DependencyObject element) where T : FrameworkElement
    {
        T correctlyTyped = element as T;

        if (correctlyTyped != null)
            return correctlyTyped;

        if (element != null)
        {
            int numChildren = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < numChildren; i++)
            {
                T child = (VisualTreeHelper.GetChild(element, i) as FrameworkElement).GetElement<T>();
                if (child != null)
                    return child;
            }

            Popup popup = element as Popup;

            if (popup != null)
                return (popup.Child as FrameworkElement).GetElement<T>();
        }

        return null;
    }

}
