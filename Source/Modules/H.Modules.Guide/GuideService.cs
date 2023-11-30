// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using System.Threading.Tasks;
using System.Threading;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using H.Providers.Ioc;

namespace H.Modules.Guide
{
    public class GuideService : IGuideService
    {
        public void Show(UIElement owner = null)
        {
            if (this.CanShow() == false)
                this.Close();
            var child = owner ?? Application.Current.MainWindow.Content as UIElement;
            var layer = AdornerLayer.GetAdornerLayer(child);
            var adorner = new GuideBoxAdorner(child, () => this.Close());
            layer.Add(adorner);
        }

        bool CanShow(UIElement owner = null)
        {
            return Application.Current.Dispatcher.Invoke(() =>
              {
                  var child = owner ?? Application.Current.MainWindow.Content as UIElement;
                  var layer = AdornerLayer.GetAdornerLayer(child);
                  var adorners = layer.GetAdorners(child)?.OfType<GuideBoxAdorner>();
                  return adorners == null || adorners.Count() == 0;
              });
        }

        void Close()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var child = Application.Current.MainWindow.Content as UIElement;
                var layer = AdornerLayer.GetAdornerLayer(child);
                var adorners = layer.GetAdorners(child).OfType<GuideBoxAdorner>();
                foreach (var adorner in adorners)
                {
                    layer.Remove(adorner);
                }
            });
        }

    }
}
