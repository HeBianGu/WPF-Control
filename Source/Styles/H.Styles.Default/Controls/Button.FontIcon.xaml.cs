using System.Windows;
using System.Windows.Controls;

namespace H.Styles.Default
{
    public class FontIconButton : Button
    {
        static FontIconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIconButton), new FrameworkPropertyMetadata(typeof(FontIconButton)));
        }
    }

    public class FontIconButtonKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(FontIconButtonKeys), "S.FontIconButton.Default");

        public static ComponentResourceKey Command => new ComponentResourceKey(typeof(FontIconButtonKeys), "S.FontIconButton.Command");

    }
}
