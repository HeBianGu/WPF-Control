// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Attach;
using H.Modules.Guide.Base;
using H.Services.Common.Guide;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Windows.Threading;

namespace H.Modules.Guide;

public class GuideService : IGuideService
{
    private IOptions<GuideOptions> _options;
    public GuideService(IOptions<GuideOptions> options)
    {
        this._options = options;
    }
    private UIElement _adonerElment;

    public bool Load(out string message)
    {
        message = null;
        if (!this._options.Value.UseOnLoad)
            return true;
        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(async () =>
        {
            var version = Assembly.GetEntryAssembly().GetName().Version;
            await Ioc<IGuideService>.Instance.Show(x =>
            {
                if (Version.TryParse(this._options.Value.Version, out Version savedVersion) == false)
                    return true;
                var cversion = Cattach.GetGuideAssemblyVersion(x);
                return cversion > savedVersion;
            });
            this._options.Value.Version = version.ToString();
            this._options.Value.Save(out string message);
        }));
        message = null;
        return true;
    }

    private TaskCompletionSource<bool> _taskCompletionSource;
    public Task Show(Predicate<UIElement> predicate = null, UIElement owner = null)
    {
        _taskCompletionSource = new TaskCompletionSource<bool>();
        this._adonerElment = GetAdornerElement(owner);
        if (this.CanShow() == false)
            this.Close();
        //Application.Current.MainWindow.Content as UIElement;
        AdornerLayer layer = AdornerLayer.GetAdornerLayer(this._adonerElment);
        GuideBoxAdorner adorner = new GuideBoxAdorner(this._adonerElment, () => this.Close(), predicate);
        layer.Add(adorner);
        return _taskCompletionSource.Task;
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
              if (layer == null)
                  return false;
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
            _taskCompletionSource.SetResult(true);
        });
    }

}
