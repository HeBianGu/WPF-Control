// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace H.Windows.Ribbon
{
    //public class DialogWindowService : IDialogWindowService
    //{
    //    //public bool ShowDialog(string messge, string title = null, int closeTime = -1, bool showEffect = true, params Tuple<string, Action>[] acts)
    //    //{
    //    //    return DialogWindow.ShowDialog(messge, title, closeTime, showEffect, acts);
    //    //}

    //    ///// <summary> 显示窗口 </summary>
    //    //public int ShowDialogWith(string messge, string title = null, bool showEffect = false, params Tuple<string, Action<IDialogWindow>>[] acts)
    //    //{
    //    //    return DialogWindow.ShowDialogWith(messge, title, showEffect, acts);
    //    //}

    //    ///// <summary> 只有确定的按钮 </summary>
    //    //public bool ShowSumit(string messge, string title = null, bool showEffect = false, int closeTime = -1)
    //    //{
    //    //    return DialogWindow.ShowSumit(messge, title, showEffect, closeTime);
    //    //}

    //}

    public static class Commander
    {
        public static ChangedImageCommand ChangedImage { get; set; } = new ChangedImageCommand();

        public static MovePreviousCommand MovePrevious { get; set; } = new MovePreviousCommand();

        public static MoveNextCommand MoveNext { get; set; } = new MoveNextCommand();
    }

    public class ChangedImageCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //if (parameter is ControlBindableBase data)
            //{
            //    var r = IocMessage.IOFileDialog.ShowOpenImageFile(x =>
            //    {
            //        Uri uri = new Uri(x, UriKind.RelativeOrAbsolute);
            //    });

            //    //data.LargeImage = uri;
            //}
        }
    }

    public class MovePreviousCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is FrameworkElement d)
            {
                IEnumerable source = d.GetParent<ItemsControl>().GetParent<ItemsControl>()?.ItemsSource;

                object item = d.DataContext;

                if (source is IList list)
                {
                    int index = list.IndexOf(item);

                    list.RemoveAt(index);

                    index = Math.Max(0, index - 1);

                    list.Insert(index, item);
                }
            }
        }
    }

    public class MoveNextCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is FrameworkElement d)
            {
                IEnumerable source = d.GetParent<ItemsControl>().GetParent<ItemsControl>()?.ItemsSource;

                object item = d.DataContext;

                if (source is IList list)
                {
                    int index = list.IndexOf(item);

                    list.RemoveAt(index);

                    index = Math.Min(list.Count, index + 1);

                    list.Insert(index, item);
                }
            }

            CommandManager.InvalidateRequerySuggested();
        }
    }

    internal static class VisualTreeExtention
    {
        public static T GetParent<T>(this DependencyObject element) where T : DependencyObject
        {
            if (element == null) return null;
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            while ((parent != null) && !(parent is T))
            {
                DependencyObject newVisualParent = VisualTreeHelper.GetParent(parent);
                if (newVisualParent != null)
                {
                    parent = newVisualParent;
                }
                else
                {
                    // try to get the logical parent ( e.g. if in Popup)
                    if (parent is FrameworkElement)
                    {
                        parent = (parent as FrameworkElement).Parent;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return parent as T;
        }
    }

    public static class ExtendRibbonCommands
    {
        public static RoutedUICommand ReorderQuickAccessToolBarCommand { get; set; } = new RoutedUICommand();
    }
}
