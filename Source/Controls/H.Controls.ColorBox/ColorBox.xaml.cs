using System.Windows;
using System.Windows.Controls.Primitives;

namespace H.Controls.ColorBox
{
    public class ColorBox : Selector
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ColorBox), "S.ColorBox.Default");

        static ColorBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorBox), new FrameworkPropertyMetadata(typeof(ColorBox)));
        }
    }
}
