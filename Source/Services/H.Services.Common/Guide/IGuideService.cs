namespace H.Services.Common
{
    public interface IGuideService
    {
        Task Show(Predicate<UIElement> predicate = null, UIElement owner = null);
    }

}
