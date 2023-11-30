using System.Windows;

namespace H.Providers.Ioc
{
    public interface IGuideService
    {
        void Show(UIElement owner = null);
    }

}
