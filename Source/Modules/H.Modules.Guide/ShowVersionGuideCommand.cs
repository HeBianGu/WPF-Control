using H.Common.Attributes;
using H.Common.Commands;
using H.Extensions.Attach;
using H.Services.Common.Guide;

namespace H.Modules.Guide;
[Icon("\xE75A")]
[Display(Name = "版本新增功能", Description = "显示版本新增功能向导")]
public class ShowVersionGuideCommand : DisplayMarkupCommandBase
{
    public string Version { get; set; }
    public override async Task ExecuteAsync(object parameter)
    {
        var version = Version ?? parameter?.ToString();
        if (version == null)
        {
            await Ioc<IGuideService>.Instance.Show();
        }
        else
        {
            await Ioc<IGuideService>.Instance.Show(x => Cattach.GetGuideAssemblyVersion(x) == version || version == "1.0.0.0");
        }
    }
}
