// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.PropertyGrid
{
    public class IntegerUpDown : CommonNumericUpDown<int>
    {
        #region Constructors

        static IntegerUpDown()
        {
            UpdateMetadata(typeof(IntegerUpDown), 1, int.MinValue, int.MaxValue);
        }

        public IntegerUpDown()
          : base(Int32.TryParse, Decimal.ToInt32, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override int IncrementValue(int value, int increment)
        {
            return value + increment;
        }

        protected override int DecrementValue(int value, int increment)
        {
            return value - increment;
        }

        #endregion //Base Class Overrides
    }
}
