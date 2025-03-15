global using H.Iocable;
global using H.Services.Common;

namespace H.Modules.Guide;

public abstract class ShowGuideCommandBase : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await Ioc<IGuideService>.Instance.Show(this.IsMatch);
    }

    protected abstract bool IsMatch(UIElement element);
}
