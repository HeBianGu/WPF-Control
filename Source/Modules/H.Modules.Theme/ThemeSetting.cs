using H.Controls.Form;
using H.Extensions.Setting;
using H.Services.Common;
using H.Themes.Default;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Windows;
using System.Xml.Serialization;

namespace H.Modules.Theme
{
    [Display(Name = "主题设置", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息")]
    public class ThemeSetting : Settable<ThemeSetting>, ILoginedSplashLoad
    {
        public ThemeSetting()
        {
            this.ColorResources.Add(new DarkColorResource());
            this.ColorResources.Add(new DefaultColorResource());
            this.ColorResources.Add(new LightColorResource());
        }

        [DefaultValue(FontSizeThemeType.Default)]
        [Display(Name = "字体")]
        public FontSizeThemeType FontSize { get; set; }

        [DefaultValue(LayoutThemeType.Default)]
        [Display(Name = "布局")]
        public LayoutThemeType Layout { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [BindingGetSelectSourceProperty(nameof(ColorResources))]
        [PropertyItem(typeof(OnlyComboBoxSelectSourcePropertyItem))]
        [Display(Name = "颜色主题")]
        public IColorResource ColorResource { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [Browsable(false)]
        public List<IColorResource> ColorResources { get; } = new List<IColorResource>();

        [Browsable(false)]
        public int ColorResourceSelectedIndex { get; set; }

        public void OnColorResourceValueChanged(PropertyInfo property, IColorResource o, IColorResource n)
        {
            //this.Color.ChangeThemeType();
            //this.ChangeColorTheme();
            this.RefreshTheme();
        }
        public void OnFontSizeValueChanged(PropertyInfo property, FontSizeThemeType o, FontSizeThemeType n)
        {
            this.FontSize.ChangeFontSizeThemeType();
        }
        public void OnLayoutValueChanged(PropertyInfo property, LayoutThemeType o, LayoutThemeType n)
        {
            this.Layout.ChangeLayoutThemeType();
        }

        public override bool Save(out string message)
        {
            //this.RefreshTheme();
            //{
            //    ResourceDictionary brushResource = new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Styles.Default;component/ConciseControls.xaml", UriKind.Absolute) };
            //    brushResource.ChangeResourceDictionary(x => x.Source.AbsoluteUri == brushResource.Source.AbsoluteUri, true);
            //}
            this.ColorResourceSelectedIndex = this.ColorResources.IndexOf(this.ColorResource);
            return base.Save(out message);
        }

        public override bool Load(out string message)
        {
            var r = base.Load(out message);
            this.ColorResource = this.ColorResources[this.ColorResourceSelectedIndex];
            this.RefreshTheme();
            return r;
        }

        private void RefreshTheme()
        {
            //this.Color.ChangeThemeType();
            this.FontSize.ChangeFontSizeThemeType();
            this.Layout.ChangeLayoutThemeType();
            this.ChangeColorTheme();
            ThemeTypeExtension.RefreshBrushResourceDictionary();
        }

        private void ChangeColorTheme()
        {
            ResourceDictionary resource = this.ColorResource.Resource;
            ThemeTypeExtension.ChangeResourceDictionary(resource, x =>
            {
                return this.ColorResources.Any(l => l.Resource.Source == x.Source);
            });
        }

        protected override string GetDefaultFolder()
        {
            return AppPaths.Instance.UserSetting;
        }

    }
}
