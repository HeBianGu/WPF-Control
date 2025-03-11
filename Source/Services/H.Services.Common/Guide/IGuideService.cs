namespace H.Services.Common
{
    public interface IGuideService
    {
        Task Show(string version = null, UIElement owner = null);
    }

}
