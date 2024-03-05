// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace H.Controls.PropertyGrid
{
    public class TrimmedTextBlock : TextBlock
    {
        #region Constructor

        public TrimmedTextBlock()
        {
            this.SizeChanged += this.TrimmedTextBlock_SizeChanged;
        }

        #endregion

        #region IsTextTrimmed Property

        public static readonly DependencyProperty IsTextTrimmedProperty = DependencyProperty.Register("IsTextTrimmed", typeof(bool), typeof(TrimmedTextBlock), new PropertyMetadata(false, OnIsTextTrimmedChanged));
        public bool IsTextTrimmed
        {
            get
            {
                return (bool)GetValue(IsTextTrimmedProperty);
            }
            private set
            {
                SetValue(IsTextTrimmedProperty, value);
            }
        }

        private static void OnIsTextTrimmedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TrimmedTextBlock textBlock = d as TrimmedTextBlock;
            if (textBlock != null)
            {
                textBlock.OnIsTextTrimmedChanged((bool)e.OldValue, (bool)e.NewValue);
            }
        }

        private void OnIsTextTrimmedChanged(bool oldValue, bool newValue)
        {
            this.ToolTip = newValue ? this.Text : null;
        }

        #endregion

        #region HighlightedBrush

        public static readonly DependencyProperty HighlightedBrushProperty = DependencyProperty.Register("HighlightedBrush", typeof(Brush), typeof(TrimmedTextBlock), new FrameworkPropertyMetadata(Brushes.Yellow));

        public Brush HighlightedBrush
        {
            get
            {
                return (Brush)GetValue(HighlightedBrushProperty);
            }
            set
            {
                SetValue(HighlightedBrushProperty, value);
            }
        }

        #endregion

        #region HighlightedText

        public static readonly DependencyProperty HighlightedTextProperty = DependencyProperty.Register("HighlightedText", typeof(string), typeof(TrimmedTextBlock), new FrameworkPropertyMetadata(null, HighlightedTextChanged));

        public string HighlightedText
        {
            get
            {
                return (string)GetValue(HighlightedTextProperty);
            }
            set
            {
                SetValue(HighlightedTextProperty, value);
            }
        }

        private static void HighlightedTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TrimmedTextBlock trimmedTextBlock = sender as TrimmedTextBlock;
            if (trimmedTextBlock != null)
            {
                trimmedTextBlock.HighlightedTextChanged((string)e.OldValue, (string)e.NewValue);
            }
        }

        protected virtual void HighlightedTextChanged(string oldValue, string newValue)
        {
            if (this.Text.Length == 0)
                return;

            // Set original text without highlight.
            if (newValue == null)
            {
                Run newrRun = new Run(this.Text);
                this.Inlines.Clear();
                this.Inlines.Add(newrRun);

                return;
            }

            int startHighlightedIndex = this.Text.IndexOf(newValue, StringComparison.InvariantCultureIgnoreCase);
            int endHighlightedIndex = startHighlightedIndex + newValue.Length;

            string startUnHighlightedText = this.Text.Substring(0, startHighlightedIndex);
            string highlightedText = this.Text.Substring(startHighlightedIndex, newValue.Length);
            string endUnHighlightedText = this.Text.Substring(endHighlightedIndex, this.Text.Length - endHighlightedIndex);

            this.Inlines.Clear();

            // Start Un-Highlighted text
            Run run = new Run(startUnHighlightedText);
            this.Inlines.Add(run);

            // Highlighted text
            run = new Run(highlightedText);
            run.Background = this.HighlightedBrush;
            this.Inlines.Add(run);

            // End Un-Highlighted text
            run = new Run(endUnHighlightedText);
            this.Inlines.Add(run);
        }

        #endregion

        #region Event Handler

        private void TrimmedTextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null)
            {
                this.IsTextTrimmed = this.GetIsTextTrimmed(textBlock);
            }
        }

        #endregion

        #region Private Methods

        private bool GetIsTextTrimmed(TextBlock textBlock)
        {
            if (textBlock == null)
                return false;
            if (textBlock.TextTrimming == TextTrimming.None)
                return false;
            if (textBlock.TextWrapping != TextWrapping.NoWrap)
                return false;

            double textBlockActualWidth = textBlock.ActualWidth;
            textBlock.Measure(new Size(double.MaxValue, double.MaxValue));
            double textBlockDesiredWidth = textBlock.DesiredSize.Width;

            return textBlockActualWidth < textBlockDesiredWidth;
        }

        #endregion
    }
}
