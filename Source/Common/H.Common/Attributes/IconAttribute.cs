namespace H.Common.Attributes;

public class IconAttribute : Attribute
{
    public IconAttribute(string icon)
    {
        this.Icon = icon;
    }
    public string Icon { get; set; }
}
