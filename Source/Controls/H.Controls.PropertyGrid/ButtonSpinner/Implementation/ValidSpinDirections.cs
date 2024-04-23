// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.PropertyGrid
{
    /// <summary>
    /// Represents spin directions that are valid.
    /// </summary>
    [Flags]
    public enum ValidSpinDirections
    {
        /// <summary>
        /// Can not increase nor decrease.
        /// </summary>
        None = 0,

        /// <summary>
        /// Can increase.
        /// </summary>
        Increase = 1,

        /// <summary>
        /// Can decrease.
        /// </summary>
        Decrease = 2
    }
}
