// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Globalization;
using System.Windows;



namespace H.Controls.PropertyGrid
{
    public abstract class NumericUpDown<T> : UpDownBase<T>
    {
#pragma warning disable 0618

        #region Properties

        #region AutoMoveFocus

        public bool AutoMoveFocus
        {
            get
            {
                return (bool)GetValue(AutoMoveFocusProperty);
            }
            set
            {
                SetValue(AutoMoveFocusProperty, value);
            }
        }

        public static readonly DependencyProperty AutoMoveFocusProperty =
            DependencyProperty.Register("AutoMoveFocus", typeof(bool), typeof(NumericUpDown<T>), new UIPropertyMetadata(false));

        #endregion AutoMoveFocus

        #region AutoSelectBehavior

        public AutoSelectBehavior AutoSelectBehavior
        {
            get
            {
                return (AutoSelectBehavior)GetValue(AutoSelectBehaviorProperty);
            }
            set
            {
                SetValue(AutoSelectBehaviorProperty, value);
            }
        }

        public static readonly DependencyProperty AutoSelectBehaviorProperty =
            DependencyProperty.Register("AutoSelectBehavior", typeof(AutoSelectBehavior), typeof(NumericUpDown<T>),
          new UIPropertyMetadata(AutoSelectBehavior.OnFocus));

        #endregion AutoSelectBehavior PROPERTY

        #region FormatString

        public static readonly DependencyProperty FormatStringProperty = DependencyProperty.Register("FormatString", typeof(string), typeof(NumericUpDown<T>), new UIPropertyMetadata(String.Empty, OnFormatStringChanged));
        public string FormatString
        {
            get
            {
                return (string)GetValue(FormatStringProperty);
            }
            set
            {
                SetValue(FormatStringProperty, value);
            }
        }

        private static void OnFormatStringChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown<T> numericUpDown = o as NumericUpDown<T>;
            if (numericUpDown != null)
                numericUpDown.OnFormatStringChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnFormatStringChanged(string oldValue, string newValue)
        {
            if (this.IsInitialized)
            {
                this.SyncTextAndValueProperties(false, null);
            }
        }

        #endregion //FormatString

        #region Increment

        public static readonly DependencyProperty IncrementProperty = DependencyProperty.Register("Increment", typeof(T), typeof(NumericUpDown<T>), new PropertyMetadata(default(T), OnIncrementChanged, OnCoerceIncrement));
        public T Increment
        {
            get
            {
                return (T)GetValue(IncrementProperty);
            }
            set
            {
                SetValue(IncrementProperty, value);
            }
        }

        private static void OnIncrementChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown<T> numericUpDown = o as NumericUpDown<T>;
            if (numericUpDown != null)
                numericUpDown.OnIncrementChanged((T)e.OldValue, (T)e.NewValue);
        }

        protected virtual void OnIncrementChanged(T oldValue, T newValue)
        {
            if (this.IsInitialized)
            {
                SetValidSpinDirection();
            }
        }

        private static object OnCoerceIncrement(DependencyObject d, object baseValue)
        {
            NumericUpDown<T> numericUpDown = d as NumericUpDown<T>;
            if (numericUpDown != null)
                return numericUpDown.OnCoerceIncrement((T)baseValue);

            return baseValue;
        }

        protected virtual T OnCoerceIncrement(T baseValue)
        {
            return baseValue;
        }

        #endregion

        #endregion //Properties

        #region Overrides


        #endregion

        #region Methods

        protected static decimal ParsePercent(string text, IFormatProvider cultureInfo)
        {
            NumberFormatInfo info = NumberFormatInfo.GetInstance(cultureInfo);

            text = text.Replace(info.PercentSymbol, null);

            decimal result = Decimal.Parse(text, NumberStyles.Any, info);
            result = result / 100;

            return result;
        }

        #endregion //Methods
    }

#pragma warning restore 0618
}
