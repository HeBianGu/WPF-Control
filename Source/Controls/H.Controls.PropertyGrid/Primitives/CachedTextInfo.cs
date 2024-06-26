﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows.Controls;

namespace H.Controls.PropertyGrid
{
    internal class CachedTextInfo : ICloneable
    {
        private CachedTextInfo(string text, int caretIndex, int selectionStart, int selectionLength)
        {
            this.Text = text;
            this.CaretIndex = caretIndex;
            this.SelectionStart = selectionStart;
            this.SelectionLength = selectionLength;
        }

        public CachedTextInfo(TextBox textBox)
          : this(textBox.Text, textBox.CaretIndex, textBox.SelectionStart, textBox.SelectionLength)
        {
        }

        public string Text { get; private set; }
        public int CaretIndex { get; private set; }
        public int SelectionStart { get; private set; }
        public int SelectionLength { get; private set; }

        #region ICloneable Members

        public object Clone()
        {
            return new CachedTextInfo(this.Text, this.CaretIndex, this.SelectionStart, this.SelectionLength);
        }

        #endregion
    }
}
