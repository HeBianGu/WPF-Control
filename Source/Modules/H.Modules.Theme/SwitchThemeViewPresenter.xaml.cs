using H.Services.Common;
using H.Mvvm;
using Microsoft.Extensions.Options;

namespace H.Modules.Theme
{
    public class SwitchThemeViewPresenter : ISwitchThemeViewPresenter
    {
        private readonly IOptions<SwitchThemeOptions> _options;
        public SwitchThemeViewPresenter(IOptions<SwitchThemeOptions> options)
        {
            _options = options;
        }

        public RelayCommand LoadedCommand => new RelayCommand(e=>
        {
            this._options.Value.Refresh();
        });

        //public RelayCommand SwitchCommand => new RelayCommand(e=>
        //{
        //    this.Refresh();
        //    //if (e is bool b)
        //    //{
        //    //    if (b)
        //    //    {
        //    //        ThemeTypeExtension.ChangeResourceDictionary(new LightColorResource().Resource, x =>
        //    //        {
        //    //            return x.Source == new DarkColorResource().Resource.Source;
        //    //        });
        //    //    }
        //    //    else
        //    //    {
        //    //        ThemeTypeExtension.ChangeResourceDictionary(new DarkColorResource().Resource, x =>
        //    //        {
        //    //            return x.Source == new LightColorResource().Resource.Source;
        //    //        });
        //    //    }

        //    //    {
        //    //        var brushResource = new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Styles.Default;component/ConciseControls.xaml", UriKind.Absolute) };
        //    //        brushResource.ChangeResourceDictionary(x => x.Source.AbsoluteUri == brushResource.Source.AbsoluteUri, true);
        //    //    }
        //    //}
        //});


        //private void Refresh()
        //{
        //    if (this._options.Value.IsDark == false)
        //    {
        //        ThemeTypeExtension.ChangeResourceDictionary(this._options.Value.Light.Resource, x =>
        //        {
        //            return x.Source == this._options.Value.Dark.Resource.Source;
        //        });
        //    }
        //    else
        //    {
        //        ThemeTypeExtension.ChangeResourceDictionary(this._options.Value.Dark.Resource, x =>
        //        {
        //            return x.Source == this._options.Value.Light.Resource.Source;
        //        });
        //    }

        //    {
        //        ResourceDictionary brushResource = new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Styles.Default;component/ConciseControls.xaml", UriKind.Absolute) };
        //        brushResource.ChangeResourceDictionary(x => x.Source.AbsoluteUri == brushResource.Source.AbsoluteUri, true);
        //    }

        //    this._options.Value.Save(out string message);
        //}

    }
}
