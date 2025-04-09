using System.Windows.Controls.Primitives;

namespace H.Styles.Controls;


public class FontIconToggleButton : ToggleButton
{
    static FontIconToggleButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIconToggleButton), new FrameworkPropertyMetadata(typeof(FontIconToggleButton)));
    }

    public string CheckedGlyph
    {
        get { return (string)GetValue(CheckedGlyphProperty); }
        set { SetValue(CheckedGlyphProperty, value); }
    }

    public static readonly DependencyProperty CheckedGlyphProperty =
        DependencyProperty.Register("CheckedGlyph", typeof(string), typeof(FontIconToggleButton), new FrameworkPropertyMetadata(default(string)));


    public string UncheckedGlyph
    {
        get { return (string)GetValue(UncheckedGlyphProperty); }
        set { SetValue(UncheckedGlyphProperty, value); }
    }

    public static readonly DependencyProperty UncheckedGlyphProperty =
        DependencyProperty.Register("UncheckedGlyph", typeof(string), typeof(FontIconToggleButton), new FrameworkPropertyMetadata(default(string)));


}
public class FontIconToggleButtonKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(FontIconToggleButtonKeys), "S.FontIconToggleButton.Default");

    public static ComponentResourceKey Switch => new ComponentResourceKey(typeof(FontIconToggleButtonKeys), "S.FontIconToggleButton.Switch");

}
