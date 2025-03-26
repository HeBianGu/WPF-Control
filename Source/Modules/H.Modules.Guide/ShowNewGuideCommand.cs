using H.Common.Attributes;
using H.Extensions.Attach;
using System.Reflection;

namespace H.Modules.Guide;
[Icon("\xE75A")]
[Display(Name = "新增功能", Description = "显示新增功能向导")]
public class ShowNewGuideCommand : ShowGuideCommandBase
{
    protected override bool IsMatch(UIElement element)
    {
        var version = Assembly.GetEntryAssembly().GetName().Version.ToString();
        return Cattach.GetGuideAssemblyVersion(element) == version || version == "1.0.0.0";
    }
}
