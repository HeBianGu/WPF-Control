﻿using Microsoft.Xaml.Behaviors;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Controls.PDF
{
    public class AllowableCharactersTextBoxBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty RegularExpressionProperty =
             DependencyProperty.Register("RegularExpression", typeof(string), typeof(AllowableCharactersTextBoxBehavior),
             new FrameworkPropertyMetadata(".*"));
        public string RegularExpression
        {
            get => (string)base.GetValue(RegularExpressionProperty);
            set => base.SetValue(RegularExpressionProperty, value);
        }

        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(AllowableCharactersTextBoxBehavior),
            new FrameworkPropertyMetadata(int.MinValue));
        public int MaxLength
        {
            get => (int)base.GetValue(MaxLengthProperty);
            set => base.SetValue(MaxLengthProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            DataObject.AddPastingHandler(this.AssociatedObject, OnPaste);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                var text = Convert.ToString(e.DataObject.GetData(DataFormats.Text));

                if (!IsValid(text, true))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        void OnPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(e.Text, false);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            DataObject.RemovePastingHandler(this.AssociatedObject, OnPaste);
        }

        private bool IsValid(string newText, bool paste)
        {
            return !ExceedsMaxLength(newText, paste) && Regex.IsMatch(newText, this.RegularExpression);
        }

        private bool ExceedsMaxLength(string newText, bool paste)
        {
            if (this.MaxLength == 0) return false;

            return LengthOfModifiedText(newText, paste) > this.MaxLength;
        }

        private int LengthOfModifiedText(string newText, bool paste)
        {
            var countOfSelectedChars = this.AssociatedObject.SelectedText.Length;
            var caretIndex = this.AssociatedObject.CaretIndex;
            var text = this.AssociatedObject.Text;

            if (countOfSelectedChars > 0 || paste)
            {
                text = text.Remove(caretIndex, countOfSelectedChars);
                return text.Length + newText.Length;
            }
            else
            {
                var insert = Keyboard.IsKeyToggled(Key.Insert);

                return insert && caretIndex < text.Length ? text.Length : text.Length + newText.Length;
            }
        }
    }
}
