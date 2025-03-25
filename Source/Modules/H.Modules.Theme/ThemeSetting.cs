using H.Controls.Form;
using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
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
using System.Windows.Media;
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
            this.FontFamilys = Fonts.SystemFontFamilies.ToList();
            //this.FontFamilys.Add(new FontFamily("微软雅黑"));
            this.FontFamilys.Insert(0, new FontFamily("微软雅黑"));
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            //this.FontFamily = new FontFamily("微软雅黑");
            this.FontFamily = null;
        }

        private FontSizeThemeType _fontSize;
        [DefaultValue(FontSizeThemeType.Default)]
        [Display(Name = "字号")]
        public FontSizeThemeType FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                RaisePropertyChanged();
            }
        }

        private LayoutThemeType _layout;
        [DefaultValue(LayoutThemeType.Default)]
        [Display(Name = "布局")]
        public LayoutThemeType Layout
        {
            get { return _layout; }
            set
            {
                _layout = value;
                RaisePropertyChanged();
            }
        }


        private IColorResource _ColorResource;
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        [PropertyNameSourcePropertyItem(typeof(ComboBoxPropertyItem), nameof(ColorResources))]
        [Display(Name = "颜色主题")]
        public IColorResource ColorResource
        {
            get { return _ColorResource; }
            set
            {
                _ColorResource = value;
                RaisePropertyChanged();
            }
        }


        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public List<IColorResource> ColorResources { get; } = new List<IColorResource>();

      
        private FontFamily _fontFamily;
        [PropertyNameSourcePropertyItem(typeof(ComboBoxPropertyItem), nameof(FontFamilys))]
        [Display(Name = "字体")]
        [TypeConverter(typeof(FontFamilyConverter))]
        public FontFamily FontFamily
        {
            get { return _fontFamily; }
            set
            {
                _fontFamily = value;
                RaisePropertyChanged();
            }
        }

        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public List<FontFamily> FontFamilys { get; } = new List<FontFamily>();

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

        public void OnFontFamilyValueChanged(PropertyInfo property, FontFamily o, FontFamily n)
        {
            //var find= ThemeTypeExtension.GetSystemsResource();
            //if (find != null)
            //    find[SystemKeys.FontFamily] = n;
            Application.Current.Resources[SystemKeys.FontFamily] = n;
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
            this.ChangeSystem();
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

        private void ChangeSystem()
        {
            Application.Current.Resources[SystemKeys.FontFamily] = this.FontFamily;
        }

        protected override string GetDefaultFolder()
        {
            return IocAppPaths.Instance.UserSetting;
        }

    }
}
