// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace H.Controls.PropertyGrid
{
    public class VisibilityToBoolConverter : IValueConverter
    {
        #region Inverted Property

        public bool Inverted
        {
            get
            {
                return _inverted;
            }
            set
            {
                _inverted = value;
            }
        }

        private bool _inverted; //false

        #endregion

        #region Not Property

        public bool Not
        {
            get
            {
                return _not;
            }
            set
            {
                _not = value;
            }
        }

        private bool _not; //false

        #endregion

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Inverted ? this.BoolToVisibility(value) : this.VisibilityToBool(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Inverted ? this.VisibilityToBool(value) : this.BoolToVisibility(value);
        }

        private object VisibilityToBool(object value)
        {
            if (!(value is Visibility))
                throw new InvalidOperationException(ErrorMessages.GetMessage("SuppliedValueWasNotVisibility"));

            return (((Visibility)value) == Visibility.Visible) ^ Not;
        }

        private object BoolToVisibility(object value)
        {
            if (!(value is bool))
                throw new InvalidOperationException(ErrorMessages.GetMessage("SuppliedValueWasNotBool"));

            return ((bool)value ^ Not) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
