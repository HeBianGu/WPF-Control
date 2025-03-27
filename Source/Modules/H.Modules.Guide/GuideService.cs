// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Services.Common.Guide;

namespace H.Modules.Guide;

public class GuideService : IGuideService
{
    private UIElement _adonerElment;
    public Task Show(Predicate<UIElement> predicate = null, UIElement owner = null)
    {
        this._adonerElment = GetAdornerElement(owner);
        if (this.CanShow() == false)
            this.Close();
        //Application.Current.MainWindow.Content as UIElement;
        AdornerLayer layer = AdornerLayer.GetAdornerLayer(this._adonerElment);
        GuideBoxAdorner adorner = new GuideBoxAdorner(this._adonerElment, () => this.Close(), predicate);
        layer.Add(adorner);
        return Task.CompletedTask;
    }

    protected virtual UIElement GetAdornerElement(UIElement owner = null)
    {
        return owner ?? GuideExtension.GetAdornerElement();
    }

    private bool CanShow(UIElement owner = null)
    {
        return Application.Current.Dispatcher.Invoke(() =>
          {
              UIElement child = GetAdornerElement(_adonerElment);
              AdornerLayer layer = AdornerLayer.GetAdornerLayer(child);
              System.Collections.Generic.IEnumerable<GuideBoxAdorner> adorners = layer.GetAdorners(child)?.OfType<GuideBoxAdorner>();
              return adorners == null || adorners.Count() == 0;
          });
    }

    private void Close()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(this._adonerElment);
            System.Collections.Generic.IEnumerable<GuideBoxAdorner> adorners = layer.GetAdorners(this._adonerElment).OfType<GuideBoxAdorner>();
            foreach (GuideBoxAdorner adorner in adorners)
            {
                layer.Remove(adorner);
            }
        });
    }

}
