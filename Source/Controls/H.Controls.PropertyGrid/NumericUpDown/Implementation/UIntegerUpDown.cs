// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.PropertyGrid
{
    internal class UIntegerUpDown : CommonNumericUpDown<uint>
    {
        #region Constructors

        static UIntegerUpDown()
        {
            UpdateMetadata(typeof(UIntegerUpDown), 1, uint.MinValue, uint.MaxValue);
        }

        public UIntegerUpDown()
          : base(uint.TryParse, Decimal.ToUInt32, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {
        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override uint IncrementValue(uint value, uint increment)
        {
            return value + increment;
        }

        protected override uint DecrementValue(uint value, uint increment)
        {
            return value - increment;
        }

        #endregion //Base Class Overrides
    }
}
