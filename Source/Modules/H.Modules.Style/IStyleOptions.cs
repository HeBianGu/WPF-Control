using H.Styles.Default.StyleResources;

namespace H.Modules.Style;

public interface IStyleOptions
{
    IStyleResource StyleResource { get; set; }
    List<IStyleResource> StyleResources { get; set; }
}
