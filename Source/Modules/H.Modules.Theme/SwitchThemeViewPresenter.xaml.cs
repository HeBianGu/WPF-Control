using H.Services.Common;
using H.Mvvm;
using Microsoft.Extensions.Options;
using H.Mvvm.Commands;
using H.Services.Common.Theme;

namespace H.Modules.Theme
{
    public class SwitchThemeViewPresenter : ISwitchThemeViewPresenter
    {
        private readonly IOptions<ThemeOptions> _options;
        public SwitchThemeViewPresenter(IOptions<ThemeOptions> options)
        {
            _options = options;
        }
        //public RelayCommand LoadedCommand => new RelayCommand(e=>
        //{
        //    this._options.Value.Refresh();
        //});


        public RelayCommand SwitchDarkCommand => new RelayCommand(x =>
        {
            this._options.Value.SwitchDark();
        });
    }
}
