﻿using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Controls.ColorPicker.Behaviors
{
    internal class TextBoxFocusBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty SelectOnMouseClickProperty =
            DependencyProperty.Register(
                nameof(SelectOnMouseClick),
                typeof(bool),
                typeof(TextBoxFocusBehavior),
                new PropertyMetadata(true));

        public static readonly DependencyProperty ConfirmOnEnterProperty =
            DependencyProperty.Register(
                nameof(ConfirmOnEnter),
                typeof(bool),
                typeof(TextBoxFocusBehavior),
                new PropertyMetadata(true));

        public static readonly DependencyProperty DeselectOnFocusLossProperty =
            DependencyProperty.Register(
                nameof(DeselectOnFocusLoss),
                typeof(bool),
                typeof(TextBoxFocusBehavior),
                new PropertyMetadata(true));

        public bool SelectOnMouseClick
        {
            get => (bool)GetValue(SelectOnMouseClickProperty);
            set => SetValue(SelectOnMouseClickProperty, value);
        }

        public bool ConfirmOnEnter
        {
            get => (bool)GetValue(ConfirmOnEnterProperty);
            set => SetValue(ConfirmOnEnterProperty, value);
        }

        public bool DeselectOnFocusLoss
        {
            get => (bool)GetValue(DeselectOnFocusLossProperty);
            set => SetValue(DeselectOnFocusLossProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotKeyboardFocus += AssociatedObjectGotKeyboardFocus;
            AssociatedObject.GotMouseCapture += AssociatedObjectGotMouseCapture;
            AssociatedObject.LostFocus += AssociatedObject_LostFocus;
            AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObjectPreviewMouseLeftButtonDown;
            AssociatedObject.KeyUp += AssociatedObject_KeyUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotKeyboardFocus -= AssociatedObjectGotKeyboardFocus;
            AssociatedObject.GotMouseCapture -= AssociatedObjectGotMouseCapture;
            AssociatedObject.LostFocus -= AssociatedObject_LostFocus;
            AssociatedObject.PreviewMouseLeftButtonDown -= AssociatedObjectPreviewMouseLeftButtonDown;
            AssociatedObject.KeyUp -= AssociatedObject_KeyUp;
        }

        // Converts number to proper format if enter is clicked and moves focus to next object
        private void AssociatedObject_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter || !ConfirmOnEnter)
                return;

            RemoveFocus();
        }

        private void RemoveFocus()
        {
            var scope = FocusManager.GetFocusScope(AssociatedObject);
            var parent = (FrameworkElement)AssociatedObject.Parent;

            while (parent != null && parent is IInputElement element && !element.Focusable)
                parent = (FrameworkElement)parent.Parent;

            FocusManager.SetFocusedElement(scope, parent);
            Keyboard.ClearFocus();
        }

        private void AssociatedObjectGotKeyboardFocus(
            object sender,
            KeyboardFocusChangedEventArgs e)
        {
            if (SelectOnMouseClick || e.KeyboardDevice.IsKeyDown(Key.Tab))
                AssociatedObject.SelectAll();
        }

        private void AssociatedObjectGotMouseCapture(
            object sender,
            MouseEventArgs e)
        {
            if (SelectOnMouseClick)
                AssociatedObject.SelectAll();
        }

        private void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DeselectOnFocusLoss)
                AssociatedObject.Select(0, 0);
        }

        private void AssociatedObjectPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!SelectOnMouseClick)
                return;

            if (!AssociatedObject.IsKeyboardFocusWithin)
            {
                AssociatedObject.Focus();
                e.Handled = true;
            }
        }
    }
}