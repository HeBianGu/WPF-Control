
using System;

namespace H.Controls.ZoomBox
{
    public class InvalidTemplateException : Exception
    {
        #region Constructors

        public InvalidTemplateException(string message)
          : base(message)
        {
        }

        public InvalidTemplateException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

        #endregion
    }
}
