// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Providers.Ioc;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace H.Modules.Guide
{
    public class GuideService : IGuideService
    {
        public void Show(UIElement owner = null)
        {
            if (this.CanShow() == false)
                this.Close();
            UIElement child = owner ?? Application.Current.MainWindow.Content as UIElement;
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(child);
            GuideBoxAdorner adorner = new GuideBoxAdorner(child, () => this.Close());
            layer.Add(adorner);
        }

        private bool CanShow(UIElement owner = null)
        {
            return Application.Current.Dispatcher.Invoke(() =>
              {
                  UIElement child = owner ?? Application.Current.MainWindow.Content as UIElement;
                  AdornerLayer layer = AdornerLayer.GetAdornerLayer(child);
                  System.Collections.Generic.IEnumerable<GuideBoxAdorner> adorners = layer.GetAdorners(child)?.OfType<GuideBoxAdorner>();
                  return adorners == null || adorners.Count() == 0;
              });
        }

        private void Close()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                UIElement child = Application.Current.MainWindow.Content as UIElement;
                AdornerLayer layer = AdornerLayer.GetAdornerLayer(child);
                System.Collections.Generic.IEnumerable<GuideBoxAdorner> adorners = layer.GetAdorners(child).OfType<GuideBoxAdorner>();
                foreach (GuideBoxAdorner adorner in adorners)
                {
                    layer.Remove(adorner);
                }
            });
        }

    }
}
