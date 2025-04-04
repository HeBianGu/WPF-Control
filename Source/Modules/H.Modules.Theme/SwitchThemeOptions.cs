using H.Common.Interfaces;
using H.Extensions.Setting;
using H.Iocable;
using H.Services.AppPath;
using H.Services.Common;
using H.Services.Setting;
using H.Theme.Colors;
using H.Theme.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Modules.Theme
{
    [Display(Name = "明暗主题设置", GroupName = SettingGroupNames.GroupStyle, Description = "明暗主题设置的信息")]
    public class SwitchThemeOptions : IocOptionInstance<SwitchThemeOptions>, ILoginedSplashLoad
    {
        private bool _isDark = true;
        //[DefaultValue(true)]
        [Display(Name = "深主题")]
        public bool IsDark
        {
            get { return _isDark; }
            set
            {
                _isDark = value;
                RaisePropertyChanged();
                this.Refresh();
            }
        }


        [Browsable(false)]
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public IColorResource Dark { get; set; } = new DarkColorResource();

        [Browsable(false)]
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public IColorResource Light { get; set; } = new LightColorResource();

        internal void Refresh()
        {
            if (this.IsDark == false)
            {
                ThemeTypeExtension.ChangeResourceDictionary(this.Light.Resource, x =>
                {
                    return ThemeOptions.Instance.ColorResources.Any(l => l.Resource.Source == x.Source) || x.Source == this.Dark.Resource.Source;
                });
            }
            else
            {
                ThemeTypeExtension.ChangeResourceDictionary(this.Dark.Resource, x =>
                {
                    return ThemeOptions.Instance.ColorResources.Any(l => l.Resource.Source == x.Source) || x.Source == this.Light.Resource.Source;
                });
            }
            ThemeOptions.Instance.ColorResource = ThemeOptions.Instance.ColorResources.FirstOrDefault(x => x.Name == (this.IsDark ? this.Dark.Name : this.Light.Name));
            ThemeOptions.Instance.RefreshBrushResourceDictionary();
            this.Save(out string message);
        }

        protected override string GetDefaultFolder()
        {
            return AppPaths.Instance.UserSetting;
        }
    }
}
