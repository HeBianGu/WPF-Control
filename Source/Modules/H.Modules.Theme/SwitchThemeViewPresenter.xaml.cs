using H.Providers.Ioc;
using H.Providers.Mvvm;
using H.Themes.Default;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace H.Modules.Theme
{
    public class SwitchThemeViewPresenter : ISwitchThemeViewPresenter
    {
        private readonly IOptions<SwitchThemeOptions> _options;
        public SwitchThemeViewPresenter(IOptions<SwitchThemeOptions> options)
        {
            _options = options;
        }

        public RelayCommand LoadedCommand => new RelayCommand((s, e) =>
        {
            this.Refresh();
        });

        public RelayCommand SwitchCommand => new RelayCommand((s, e) =>
        {
            this.Refresh();
            //if (e is bool b)
            //{
            //    if (b)
            //    {
            //        ThemeTypeExtension.ChangeResourceDictionary(new LightColorResource().Resource, x =>
            //        {
            //            return x.Source == new DarkColorResource().Resource.Source;
            //        });
            //    }
            //    else
            //    {
            //        ThemeTypeExtension.ChangeResourceDictionary(new DarkColorResource().Resource, x =>
            //        {
            //            return x.Source == new LightColorResource().Resource.Source;
            //        });
            //    }

            //    {
            //        var brushResource = new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Styles.Default;component/ConciseControls.xaml", UriKind.Absolute) };
            //        brushResource.ChangeResourceDictionary(x => x.Source.AbsoluteUri == brushResource.Source.AbsoluteUri, true);
            //    }
            //}
        });


        private void Refresh()
        {
            if (this._options.Value.IsDark == false)
            {
                ThemeTypeExtension.ChangeResourceDictionary(this._options.Value.Light.Resource, x =>
                {
                    return x.Source == this._options.Value.Dark.Resource.Source;
                });
            }
            else
            {
                ThemeTypeExtension.ChangeResourceDictionary(this._options.Value.Dark.Resource, x =>
                {
                    return x.Source == this._options.Value.Light.Resource.Source;
                });
            }

            {
                var brushResource = new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Styles.Default;component/ConciseControls.xaml", UriKind.Absolute) };
                brushResource.ChangeResourceDictionary(x => x.Source.AbsoluteUri == brushResource.Source.AbsoluteUri, true);
            }

            this._options.Value.Save(out string message);
        }

    }
}
