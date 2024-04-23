// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.PropertyGrid
{
    internal class ULongUpDown : CommonNumericUpDown<ulong>
    {
        #region Constructors

        static ULongUpDown()
        {
            UpdateMetadata(typeof(ULongUpDown), 1, ulong.MinValue, ulong.MaxValue);
        }

        public ULongUpDown()
          : base(ulong.TryParse, Decimal.ToUInt64, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override ulong IncrementValue(ulong value, ulong increment)
        {
            return value + increment;
        }

        protected override ulong DecrementValue(ulong value, ulong increment)
        {
            return value - increment;
        }

        #endregion //Base Class Overrides
    }
}
