global using H.Iocable;
using H.Common.Commands;
using H.Services.Common.Guide;

namespace H.Modules.Guide;

public abstract class ShowGuideCommandBase : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await Ioc<IGuideService>.Instance.Show(this.IsMatch);
    }

    protected abstract bool IsMatch(UIElement element);
}
