namespace H.Styles.Controls;

public partial class ButtonKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Default");

    public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Accent");
    public static ComponentResourceKey Success => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Success");
    public static ComponentResourceKey Error => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Error");

    public static ComponentResourceKey Command => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Command");

    public static ComponentResourceKey Geometry => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Geometry");
    public static ComponentResourceKey Icon => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Icon");

    public static ComponentResourceKey GeometryError => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Geometry.Error");

    public static ComponentResourceKey Tool => new ComponentResourceKey(typeof(ButtonKeys), "S.Button.Tool");
}
