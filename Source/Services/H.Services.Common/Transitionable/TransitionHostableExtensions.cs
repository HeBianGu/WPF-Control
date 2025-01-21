using System.Windows.Controls;

namespace H.Services.Common
{
    public static class TransitionHostableExtensions
    {
        public static async Task Show(this ITransitionHostable host, UIElement visual)
        {
            visual.Visibility = Visibility.Visible;
            if (host.Transitionable == null)
                return;
            var disposable = new IsHitTestVisibleDisposable(visual);
            await host.Transitionable.Show(visual);
            disposable.Dispose();
        }

        public static async Task Close(this ITransitionHostable host, UIElement visual)
        {
            if (host.Transitionable == null)
                return;
            var disposable = new IsHitTestVisibleDisposable(visual);
            await host.Transitionable.Close(visual);
            disposable.Dispose();
            visual.Visibility = Visibility.Collapsed;
        }
    }
}
