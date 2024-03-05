// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.ComponentModel;

namespace H.Controls.PropertyGrid
{
    public class AutoCompletingMaskEventArgs : CancelEventArgs
    {
        public AutoCompletingMaskEventArgs(MaskedTextProvider maskedTextProvider, int startPosition, int selectionLength, string input)
        {
            m_autoCompleteStartPosition = -1;

            m_maskedTextProvider = maskedTextProvider;
            m_startPosition = startPosition;
            m_selectionLength = selectionLength;
            m_input = input;
        }

        #region MaskedTextProvider PROPERTY

        private MaskedTextProvider m_maskedTextProvider;

        public MaskedTextProvider MaskedTextProvider
        {
            get { return m_maskedTextProvider; }
        }

        #endregion MaskedTextProvider PROPERTY

        #region StartPosition PROPERTY

        private int m_startPosition;

        public int StartPosition
        {
            get { return m_startPosition; }
        }

        #endregion StartPosition PROPERTY

        #region SelectionLength PROPERTY

        private int m_selectionLength;

        public int SelectionLength
        {
            get { return m_selectionLength; }
        }

        #endregion SelectionLength PROPERTY

        #region Input PROPERTY

        private string m_input;

        public string Input
        {
            get { return m_input; }
        }

        #endregion Input PROPERTY


        #region AutoCompleteStartPosition PROPERTY

        private int m_autoCompleteStartPosition;

        public int AutoCompleteStartPosition
        {
            get { return m_autoCompleteStartPosition; }
            set { m_autoCompleteStartPosition = value; }
        }

        #endregion AutoCompleteStartPosition PROPERTY

        #region AutoCompleteText PROPERTY

        private string m_autoCompleteText;

        public string AutoCompleteText
        {
            get { return m_autoCompleteText; }
            set { m_autoCompleteText = value; }
        }

        #endregion AutoCompleteText PROPERTY
    }
}
