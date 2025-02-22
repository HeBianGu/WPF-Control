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
}
