
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace H.Controls.FilterColumnDataGrid
{
    public class FilterState : DependencyObject
    {
        #region Public Fields

        public static readonly DependencyProperty IsFilteredProperty = DependencyProperty.RegisterAttached("IsFiltered",
            typeof(bool), typeof(FilterState), new UIPropertyMetadata(false));

        #endregion Public Fields

        #region Public Methods

        public static bool GetIsFiltered(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFilteredProperty);
        }

        public static void SetIsFiltered(DependencyObject obj, bool value)
        {
            obj?.SetValue(IsFilteredProperty, value);
        }

        #endregion Public Methods
    }
    public static class ScrollToTopBehavior
    {
        #region Public Fields

        public static readonly DependencyProperty ScrollToTopProperty =
            DependencyProperty.RegisterAttached
            (
                "ScrollToTop",
                typeof(bool),
                typeof(ScrollToTopBehavior),
                new UIPropertyMetadata(false, OnScrollToTopPropertyChanged)
            );

        #endregion Public Fields

        #region Public Methods

        public static bool GetScrollToTop(DependencyObject obj)
        {
            return (bool)obj.GetValue(ScrollToTopProperty);
        }

        public static void SetScrollToTop(DependencyObject obj, bool value)
        {
            obj.SetValue(ScrollToTopProperty, value);
        }

        #endregion Public Methods

        #region Private Methods

        private static void ItemsSourceChanged(object o, EventArgs eArgs)
        {
            ItemsControl itemsControl = o as ItemsControl;

            if (itemsControl == null) return;

            void EventHandler(object sender, EventArgs e)
            {
                if (itemsControl.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                {
                    ScrollViewer scrollViewer = itemsControl.FindVisualChild<ScrollViewer>();
                    scrollViewer.ScrollToTop();
                    itemsControl.ItemContainerGenerator.StatusChanged -= EventHandler;
                }
            }

            itemsControl.ItemContainerGenerator.StatusChanged += EventHandler;
        }

        private static void OnScrollToTopPropertyChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            ItemsControl itemsControl = dpo as ItemsControl;

            if (itemsControl != null)
            {
                DependencyPropertyDescriptor dependencyPropertyDescriptor =
                    DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(ItemsControl));

                if (dependencyPropertyDescriptor != null)
                {
                    if ((bool)e.NewValue)
                    {
                        dependencyPropertyDescriptor.AddValueChanged(itemsControl, ItemsSourceChanged);
                    }
                    else
                    {
                        dependencyPropertyDescriptor.RemoveValueChanged(itemsControl, ItemsSourceChanged);
                    }
                }
            }
        }

        #endregion Private Methods
    }

    public static class Extensions
    {
        #region Public Methods

        public static bool IsSystemType(this Type type) => type.Assembly == typeof(object).Assembly;

        public static object GetPropertyValue(this object obj, string propertyName)
        {
            if (obj == null) throw new ArgumentException("Value cannot be null.", nameof(obj));
            if (propertyName == null) throw new ArgumentException("Value cannot be null.", nameof(propertyName));

            foreach (var prop in propertyName.Split('.').Select(s => obj?.GetType().GetProperty(s)))
                obj = prop?.GetValue(obj, null);
            return obj;
        }

        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            foreach (var prop in propertyName.Split('.').Select(s => obj?.GetType().GetProperty(s)))
                obj = prop?.GetValue(obj, null);

            return obj != null ? (T)obj : default;
        }

        public static PropertyInfo GetPropertyInfo(this Type srcType, string propertyName)
        {
            if (srcType == null) throw new ArgumentException("Value cannot be null.", nameof(srcType));
            if (propertyName == null) throw new ArgumentException("Value cannot be null.", nameof(propertyName));

            PropertyInfo infos = null;

            if (!propertyName.Contains('.')) return srcType.GetProperty(propertyName);

            foreach (var info in propertyName.Split('.')
                         .Select(s => srcType?.GetProperty(s, BindingFlags.Public | BindingFlags.Instance)))
            {
                srcType = info?.PropertyType;
                if (srcType == null) break;
                infos = info;
            }

            return infos;
        }

        #endregion Public Methods
    }

    public static class Helpers
    {
        #region Public Methods
        public static void Elapsed(string label, DateTime start)
        {
            var span = DateTime.Now - start;
            Debug.WriteLine($"{label,-20}{span:mm\\:ss\\.ff}");
        }

        #endregion Public Methods
    }

    public static class JsonConvert
    {
        private static DataContractJsonSerializerSettings GetSettings() =>
            new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-ddTHH:mm:ss.fffffff")
            };

        public static T Deserialize<T>(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                    return default;

                using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var ser = new DataContractJsonSerializer(typeof(T), GetSettings());
                    return (T)ser.ReadObject(fs);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"JsonConvert.Deserialize error : {ex.Message}");
                throw;
            }
        }

        public static long Serialize<T>(string filename, T data)
        {
            try
            {
                using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (var writer = JsonReaderWriterFactory.CreateJsonWriter(fs, Encoding.UTF8, true, false, "  "))
                    {
                        var ser = new DataContractJsonSerializer(typeof(T), GetSettings());
                        ser.WriteObject(writer, data);
                        writer.Flush();
                        return fs.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"JsonConvert.Serialize error : {ex.Message}");
                throw;
            }
        }
    }

    public static class VisualTreeHelpers
    {
        #region Private Methods

        public static T FindVisualChild<T>(this DependencyObject dependencyObject, string name)
            where T : DependencyObject
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(dependencyObject);

            //http://stackoverflow.com/questions/12304904/why-visualtreehelper-getchildrencount-returns-0-for-popup

            if (childrenCount == 0 && dependencyObject is Popup)
            {
                var popup = dependencyObject as Popup;
                return popup.Child?.FindVisualChild<T>(name);
            }

            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(dependencyObject, i);
                var nameOfChild = child.GetValue(FrameworkElement.NameProperty) as string;

                if (child is T && (name == string.Empty || name == nameOfChild))
                    return (T)child;
                var childOfChild = child.FindVisualChild<T>(name);
                if (childOfChild != null)
                    return childOfChild;
            }

            return null;
        }

        private static IEnumerable<T> GetChildrenOf<T>(this DependencyObject obj, bool recursive)
            where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(obj);
            for (var i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is T) yield return (T)child;

                if (recursive)
                    foreach (var item in child.GetChildrenOf<T>())
                        yield return item;
            }
        }

        private static IEnumerable<T> GetChildrenOf<T>(this DependencyObject obj) where T : DependencyObject
        {
            return obj.GetChildrenOf<T>(false);
        }
        private static DependencyObject GetParentObject(this DependencyObject child)
        {
            if (child == null)
                return null;

            var contentElement = child as ContentElement;
            if (contentElement != null)
            {
                var parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                var fce = contentElement as FrameworkContentElement;
                return fce?.Parent;
            }
            var frameworkElement = child as FrameworkElement;
            if (frameworkElement != null)
            {
                var parent = frameworkElement.Parent;
                if (parent != null) return parent;
            }
            return VisualTreeHelper.GetParent(child);
        }

        #endregion Private Methods

        #region Public Methods
        public static T FindAncestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            current = VisualTreeHelper.GetParent(current);

            while (current != null)
            {
                if (current is T) return (T)current;

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }

        public static T FindAncestor<T>(DependencyObject current, T lookupItem)
            where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T && current == lookupItem) return (T)current;

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }

        public static T FindAncestor<T>(DependencyObject current, string parentName)
            where T : DependencyObject
        {
            while (current != null)
            {
                if (!string.IsNullOrEmpty(parentName))
                {
                    var frameworkElement = current as FrameworkElement;
                    if (current is T && frameworkElement != null && frameworkElement.Name == parentName)
                        return (T)current;
                }
                else if (current is T)
                {
                    return (T)current;
                }

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }

        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var childType = child as T;
                if (childType == null)
                {
                    foundChild = FindChild<T>(child, childName);
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                    foundChild = FindChild<T>(child, childName);
                    if (foundChild != null) break;
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        public static T FindChild<T>(DependencyObject parent)
            where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                var childType = child as T;
                if (childType == null)
                {
                    foundChild = FindChild<T>(child);
                    if (foundChild != null) break;
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        public static T FindVisualChild<T>(this DependencyObject dependencyObject) where T : DependencyObject
        {
            return dependencyObject.FindVisualChild<T>(string.Empty);
        }

        public static Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null) return null;
            if (element.GetType() == type) return element;
            Visual foundElement = null;
            if (element is FrameworkElement frameworkElement) frameworkElement.ApplyTemplate();
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                var visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null) break;
            }

            return foundElement;
        }

        public static DataGridColumnHeader GetHeader(DataGridColumn column, DependencyObject reference)
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(reference); i++)
            {
                var child = VisualTreeHelper.GetChild(reference, i);

                if (child is DataGridColumnHeader colHeader && colHeader.Column == column) return colHeader;

                colHeader = GetHeader(column, child);
                if (colHeader != null) return colHeader;
            }

            return null;
        }

        public static T TryFindParent<T>(this DependencyObject child) where T : DependencyObject
        {
            var parentObject = child.GetParentObject();
            if (parentObject == null)
                return null;
            var parent = parentObject as T;
            if (parent != null)
                return parent;
            return parentObject.TryFindParent<T>();
        }

        #endregion Public Methods
    }

    [Serializable]
    public abstract class NotifyProperty : INotifyPropertyChanged
    {
        #region Public Events
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Private Methods
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                Debug.Fail("Invalid property name: " + propertyName);
        }

        #endregion Private Methods

        #region Public Methods
        public void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Public Methods
    }
}