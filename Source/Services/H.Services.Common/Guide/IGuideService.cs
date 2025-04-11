namespace H.Services.Common.Guide;

public interface IGuideService : IMainWindowLoadedLoadable
{
    Task Show(Predicate<UIElement> predicate = null, UIElement owner = null);
}
