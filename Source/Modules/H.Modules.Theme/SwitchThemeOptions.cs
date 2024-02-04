using H.Extensions.Setting;
using H.Providers.Ioc;
using H.Themes.Default;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Windows;
using System.Xml.Serialization;

namespace H.Modules.Theme
{
    [Display(Name = "主题设置", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息")]
    public class SwitchThemeOptions : IocOptionInstance<SwitchThemeOptions>, ILoginedSplashLoad
    {
        private bool _isDark=true;
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
        [JsonIgnore]
        [XmlIgnore]
        public IColorResource Dark { get; set; } = new DarkColorResource();

        [Browsable(false)]
        [JsonIgnore]
        [XmlIgnore]
        public IColorResource Light { get; set; } = new LightColorResource();

        internal void Refresh()
        {
            if (this.IsDark == false)
            {
                ThemeTypeExtension.ChangeResourceDictionary(this.Light.Resource, x =>
                {
                    return x.Source == this.Dark.Resource.Source;
                });
            }
            else
            {
                ThemeTypeExtension.ChangeResourceDictionary(this.Dark.Resource, x =>
                {
                    return x.Source == this.Light.Resource.Source;
                });
            }

            {
                ResourceDictionary brushResource = new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Styles.Default;component/ConciseControls.xaml", UriKind.Absolute) };
                brushResource.ChangeResourceDictionary(x => x.Source.AbsoluteUri == brushResource.Source.AbsoluteUri, true);
            }

            this.Save(out string message);
        }

        protected virtual string GetDefaultFolder()
        {
            return AppPaths.Instance.UserSetting;
        }
    }
}
