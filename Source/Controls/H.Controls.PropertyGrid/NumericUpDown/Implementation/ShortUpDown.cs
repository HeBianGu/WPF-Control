// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.PropertyGrid
{
    public class ShortUpDown : CommonNumericUpDown<short>
    {
        #region Constructors

        static ShortUpDown()
        {
            UpdateMetadata(typeof(ShortUpDown), 1, short.MinValue, short.MaxValue);
        }

        public ShortUpDown()
          : base(Int16.TryParse, Decimal.ToInt16, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override short IncrementValue(short value, short increment)
        {
            return (short)(value + increment);
        }

        protected override short DecrementValue(short value, short increment)
        {
            return (short)(value - increment);
        }

        #endregion //Base Class Overrides
    }
}
