
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace H.Controls.Dock.Controls
{
    public static class Extensions
    {
        /// <summary>Finds all the children in the visual tree of a specific type from a certain point.</summary>
        /// <typeparam name="T">The type to search for.</typeparam>
        /// <param name="depObj">The starting point for the search.</param>
        /// <returns>All the children found.</returns>
        /// <remarks>Uses <see cref="VisualTreeHelper"/> internally.</remarks>
        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T t)
                    yield return t;
                foreach (T childOfChild in FindVisualChildren<T>(child))
                    yield return childOfChild;
            }
        }

        /// <summary>Finds all the children in the logical tree of a specific type from a certain point.</summary>
        /// <typeparam name="T">The type to search for.</typeparam>
        /// <param name="depObj">The starting point for the search.</param>
        /// <returns>All the children found.</returns>
        /// <remarks>Uses <see cref="LogicalTreeHelper"/> internally.</remarks>
        public static IEnumerable<T> FindLogicalChildren<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;
            foreach (DependencyObject child in LogicalTreeHelper.GetChildren(depObj).OfType<DependencyObject>())
            {
                if (child is T t)
                    yield return t;
                foreach (T childOfChild in FindLogicalChildren<T>(child))
                    yield return childOfChild;
            }
        }

        /// <summary>Finds the visual root in the visual/logical tree.</summary>
        /// <param name="initial">The stating element.</param>
        /// <returns>The root for this branch.</returns>
        /// <remarks>Uses <see cref="LogicalTreeHelper.GetParent"/> and <see cref="VisualTreeHelper.GetParent"/> internally.</remarks>
        public static DependencyObject FindVisualTreeRoot(this DependencyObject initial)
        {
            DependencyObject current = initial;
            DependencyObject result = initial;
            while (current != null)
            {
                result = current;
                if (current is Visual || current is Visual3D)
                    current = VisualTreeHelper.GetParent(current);
                else
                {
                    // If we're in Logical Land then we must walk
                    // up the logical tree until we find a
                    // Visual/Visual3D to get us back to Visual Land.
                    current = LogicalTreeHelper.GetParent(current);
                }
            }
            return result;
        }

        /// <summary>Finds the first ancestor of a specific type in the visual tree.</summary>
        /// <typeparam name="T">The type to search for.</typeparam>
        /// <param name="dependencyObject">The dependency object to find ancestor for.</param>
        /// <returns>The ancestor, or <c>null</c> if no match found.</returns>
        /// <remarks>Uses <see cref="VisualTreeHelper.GetParent"/> internally.</remarks>
        public static T FindVisualAncestor<T>(this DependencyObject dependencyObject) where T : class
        {
            DependencyObject target = dependencyObject;
            do
            {
                target = VisualTreeHelper.GetParent(target);
            }
            while (target != null && !(target is T));
            return target as T;
        }

        /// <summary>Finds the first ancestor of a specific type in the logical tree / visual tree.</summary>
        /// <typeparam name="T">The type to search for.</typeparam>
        /// <param name="dependencyObject">The dependency object to find ancestor for.</param>
        /// <returns>The ancestor, or <c>null</c> if no match found.</returns>
        /// <remarks>Uses <see cref="LogicalTreeHelper.GetParent"/> and <see cref="VisualTreeHelper.GetParent"/> internally.</remarks>
        public static T FindLogicalAncestor<T>(this DependencyObject dependencyObject) where T : class
        {
            DependencyObject target = dependencyObject;
            do
            {
                DependencyObject current = target;
                target = LogicalTreeHelper.GetParent(target) ?? VisualTreeHelper.GetParent(current);
            }
            while (target != null && !(target is T));
            return target as T;
        }
    }
}