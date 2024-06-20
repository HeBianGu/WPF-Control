// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Controls.PropertyGrid
{
    /// <summary>
    /// Provides data for the Spinner.Spin event.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    public class SpinEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// Gets the SpinDirection for the spin that has been initiated by the 
        /// end-user.
        /// </summary>
        public SpinDirection Direction
        {
            get;
            private set;
        }

        /// <summary>
        /// Get or set whheter the spin event originated from a mouse wheel event.
        /// </summary>
        public bool UsingMouseWheel
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the SpinEventArgs class.
        /// </summary>
        /// <param name="direction">Spin direction.</param>
        public SpinEventArgs(SpinDirection direction)
          : base()
        {
            this.Direction = direction;
        }

        public SpinEventArgs(RoutedEvent routedEvent, SpinDirection direction)
          : base(routedEvent)
        {
            this.Direction = direction;
        }

        public SpinEventArgs(SpinDirection direction, bool usingMouseWheel)
          : base()
        {
            this.Direction = direction;
            this.UsingMouseWheel = usingMouseWheel;
        }

        public SpinEventArgs(RoutedEvent routedEvent, SpinDirection direction, bool usingMouseWheel)
          : base(routedEvent)
        {
            this.Direction = direction;
            this.UsingMouseWheel = usingMouseWheel;
        }
    }
}
