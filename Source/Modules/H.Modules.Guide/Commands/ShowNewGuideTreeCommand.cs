using H.Common.Attributes;
using H.Extensions.Attach;
using System.Reflection;
namespace H.Modules.Guide.Commands;

[Icon("\xEC92")]
[Display(Name = "新增功能列表", Description = "显示版本功能列表")]
public class ShowNewGuideTreeCommand : ShowGuideTreeCommandBase
{
    protected override bool IsMatch(UIElement element)
    {
        var version = Assembly.GetEntryAssembly().GetName().Version;
        if (version == null || version.ToString() == "1.0.0.0")
            return true;
        return Cattach.GetGuideAssemblyVersion(element) >= version;
    }
}
