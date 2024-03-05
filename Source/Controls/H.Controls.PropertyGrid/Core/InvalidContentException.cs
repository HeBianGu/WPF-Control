// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.PropertyGrid
{
    public class InvalidContentException : Exception
    {
        #region Constructors

        public InvalidContentException(string message)
          : base(message)
        {
        }

        public InvalidContentException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

        #endregion
    }
}
