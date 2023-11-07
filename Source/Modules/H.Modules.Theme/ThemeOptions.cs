using H.Extensions.Setting;
using H.Providers.Ioc;
using H.Themes.Default;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace H.Modules.Theme
{
    [Display(Name = "主题设置", GroupName = SystemSetting.GroupSystem, Description = "登录页面设置的信息")]
    public class ThemeOptions : IocOptionInstance<ThemeOptions>
    {
        public string MyName { get; set; } = "我是名称";
        public ColorThemeType Color { get; set; }
        public ThemeType FontSize { get; set; }
        public ThemeType Layout { get; set; }

        public void OnColorValueChanged(PropertyInfo property, ColorThemeType o, ColorThemeType n)
        {
            this.Color.ChangeThemeType();
        }
        public void OnFontSizeValueChanged(PropertyInfo property, ThemeType o, ThemeType n)
        {
            this.FontSize.ChangeFontSizeThemeType();
        }
        public void OnLayoutValueChanged(PropertyInfo property, ThemeType o, ThemeType n)
        {
            this.Layout.ChangeLayoutThemeType();
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
        }

        public override bool Save(out string message)
        {
            this.RefreshTheme();
            {
                var brushResource = new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Styles.Default;component/ConciseControls.xaml", UriKind.Absolute) };
                brushResource.ChangeResourceDictionary(x => x.Source.AbsoluteUri == brushResource.Source.AbsoluteUri, true);

            }
            return base.Save(out message);
        }

        public override void Load()
        {
            base.Load();
            this.RefreshTheme();
        }

        private void RefreshTheme()
        {
            this.Color.ChangeThemeType();
            this.FontSize.ChangeFontSizeThemeType();
            this.Layout.ChangeLayoutThemeType();


        }
    }
}
