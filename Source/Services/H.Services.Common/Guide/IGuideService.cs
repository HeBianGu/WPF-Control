namespace H.Services.Common.Guide;

public interface IGuideService
{
    Task Show(Predicate<UIElement> predicate = null, UIElement owner = null);
}
