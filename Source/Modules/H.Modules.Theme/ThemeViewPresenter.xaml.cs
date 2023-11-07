using H.Providers.Ioc;
using H.Providers.Mvvm;
using H.Themes.Default;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace H.Modules.Theme
{
    public class ThemeViewPresenter : IThemeViewPresenter
    {
        public ThemeViewPresenter(IOptions<ThemeOptions> options)
        {
            
        }
    }
}
