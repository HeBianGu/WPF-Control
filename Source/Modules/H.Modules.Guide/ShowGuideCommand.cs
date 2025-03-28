using H.Common.Attributes;
using H.Common.Commands;
using H.Services.Common.Guide;

namespace H.Modules.Guide;

[Icon("\xE963")]
[Display(Name = "新手向导", Description = "显示新手向导")]
public class ShowGuideCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await Ioc<IGuideService>.Instance.Show();
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && Ioc<IGuideService>.Instance != null;
    }
}
