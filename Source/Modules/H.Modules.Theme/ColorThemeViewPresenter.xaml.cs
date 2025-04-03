using H.Services.Common;
using H.Mvvm;
using Microsoft.Extensions.Options;
using H.Mvvm.Commands;
using H.Services.Common.Theme;

namespace H.Modules.Theme;
public class ColorThemeViewPresenter : IColorThemeViewPresenter
{
    private readonly IOptions<ThemeOptions> _options;
    public ColorThemeViewPresenter(IOptions<ThemeOptions> options)
    {
        _options = options;
    }
    //public RelayCommand LoadedCommand => new RelayCommand(e=>
    //{
    //    this._options.Value.Refresh();
    //});
}
