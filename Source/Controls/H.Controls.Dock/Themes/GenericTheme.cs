








using System;

namespace H.Controls.Dock.Themes
{
    /// <inheritdoc/>
    public class GenericTheme : Theme
    {
        /// <inheritdoc/>
        public override Uri GetResourceUri()
        {
            return new Uri("/H.Controls.Dock;component/Themes/Generic.xaml", UriKind.Relative);
        }
    }
}