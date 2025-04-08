using H.Common.Interfaces;
using H.Controls.Form;
using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using H.Extensions.Setting;
using H.Mvvm.Commands;
using H.Services.AppPath;
using H.Services.Common;
using H.Services.Setting;
using H.Themes.Backgrounds;
using H.Themes.Colors;
using H.Themes.Extensions;
using H.Themes.Systems;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace H.Modules.Theme;
[Display(Name = "主题设置", GroupName = SettingGroupNames.GroupStyle, Description = "登录页面设置的信息")]
public class ThemeOptions : IocOptionInstance<ThemeOptions>, ILoginedSplashLoad, IThemeOptions
{
    public ThemeOptions()
    {
        this.ColorResources.Add(new DarkColorResource()); 
        this.ColorResources.Add(new LightColorResource());
        this.ColorResources.Add(new DefaultColorResource());
        this.FontFamilys = Fonts.SystemFontFamilies.ToList();
        //this.FontFamilys.Add(new FontFamily("微软雅黑"));
        this.FontFamilys.Insert(0, new FontFamily("微软雅黑"));

        this.BackgroundResources.Add(new SolidColorBrushResource());
        this.BackgroundResources.Add(new LinearGradientBrushResource());
        this.BackgroundResources.Add(new RadialGradientBrushResource());
        this.BackgroundResources.Add(new DrawingBrushResource());
        this.BackgroundResources.Add(new ImageBrushResource());
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


    private IColorResource _colorResource;
    [JsonIgnore]
    [XmlIgnore]
    [PropertyNameSourcePropertyItem(typeof(ComboBoxPropertyItem), nameof(ColorResources))]
    [Display(Name = "配色")]
    public IColorResource ColorResource
    {
        get { return _colorResource; }
        set
        {
            _colorResource = value;
            RaisePropertyChanged();
        }
    }


    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public List<IColorResource> ColorResources { get; } = new List<IColorResource>();


    private IBackgroundResource _backgroundResource;
    [JsonIgnore]
    [XmlIgnore]
    [PropertyNameSourcePropertyItem(typeof(ComboBoxPropertyItem), nameof(BackgroundResources))]
    [Display(Name = "背景配色")]
    public IBackgroundResource BackgroundResource
    {
        get { return _backgroundResource; }
        set
        {
            _backgroundResource = value;
            RaisePropertyChanged();
        }
    }

    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public List<IBackgroundResource> BackgroundResources { get; } = new List<IBackgroundResource>();


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

    private FontFamily _iconFontFamily;
    [PropertyNameSourcePropertyItem(typeof(ComboBoxPropertyItem), nameof(IconFontFamilys))]
    [Display(Name = "图标字体")]
    [TypeConverter(typeof(FontFamilyConverter))]
    public FontFamily IconFontFamily
    {
        get { return _iconFontFamily; }
        set
        {
            _iconFontFamily = value;
            RaisePropertyChanged();
        }
    }

    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public List<FontFamily> FontFamilys { get; } = new List<FontFamily>();


    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public List<FontFamily> IconFontFamilys { get; } = new List<FontFamily>();

    [JsonIgnore]
    [XmlIgnore]
    public RelayCommand RefreshThemeCommand => new RelayCommand(x =>
    {
        //if (x is IColorResource resource && resource != this.ColorResource)
        //{
        //    this.ColorResource = resource;
        this.RefreshTheme();
        //}
    });

    private bool _isDark = true;
    //[DefaultValue(true)]
    [Display(Name = "切换明暗主题")]
    public bool IsDark
    {
        get { return _isDark; }
        set
        {
            if (this._isDark == value)
                return;
            _isDark = value;
            RaisePropertyChanged();
            this.SwitchDark();
        }
    }

    private void SwitchDark()
    {
        if (this.IsDark == false)
        {
            var light = this.ColorResources.FirstOrDefault(x => x.IsDark == false);
            if (light == null)
                return;
            this.ColorResource = light;
        }
        else
        {
            var dark = this.ColorResources.FirstOrDefault(x => x.IsDark == true);
            if (dark == null)
                return;
            this.ColorResource = dark;
        }
        this.RefreshTheme();
    }

    [Browsable(false)]
    public int ColorResourceSelectedIndex { get; set; }
    public void OnColorResourceValueChanged(PropertyInfo property, IColorResource o, IColorResource n)
    {
        this.RefreshTheme();
    }


    [Browsable(false)]
    public int BackgroundResourceSelectedIndex { get; set; }
    public void OnBackgroundResourceValueChanged(PropertyInfo property, IBackgroundResource o, IBackgroundResource n)
    {
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
        //Application.Current.Resources[SystemKeys.FontFamily] = n;
        this.ChangeFontFamily();
    }

    public void OnIconFontFamilyValueChanged(PropertyInfo property, FontFamily o, FontFamily n)
    {
        //Application.Current.Resources[SystemKeys.FontFamilyIcon] = n;
        this.ChangeIconFontFamily();
    }

    public override bool Save(out string message)
    {
        this.ColorResourceSelectedIndex = this.ColorResources.IndexOf(this.ColorResource);
        this.BackgroundResourceSelectedIndex = this.BackgroundResources.IndexOf(this.BackgroundResource);
        return base.Save(out message);
    }

    public override bool Load(out string message)
    {
        var r = base.Load(out message);
        if (r == false)
        {
            if (this.ColorResource != null)
            {
                this.ColorResourceSelectedIndex = this.ColorResources.IndexOf(this.ColorResource);
                this.IsDark = this.ColorResource.IsDark;
            }
            if (this.BackgroundResource != null)
                this.BackgroundResourceSelectedIndex = this.BackgroundResources.IndexOf(this.BackgroundResource);
            
        }

        if (this.ColorResourceSelectedIndex < 0 || this.ColorResourceSelectedIndex >= this.ColorResources.Count)
            this.ColorResourceSelectedIndex = 0;
        this.ColorResource = this.ColorResources[this.ColorResourceSelectedIndex];

        if (this.BackgroundResourceSelectedIndex < 0 || this.BackgroundResourceSelectedIndex >= this.BackgroundResources.Count)
            this.BackgroundResourceSelectedIndex = 0;
        this.BackgroundResource = this.BackgroundResources[this.BackgroundResourceSelectedIndex];
        this.RefreshTheme();
        return r;
    }

    private void RefreshTheme()
    {
        //this.Color.ChangeThemeType();
        this.FontSize.ChangeFontSizeThemeType();
        this.Layout.ChangeLayoutThemeType();
        this.ChangeColorTheme();
        this.ChangeFontFamily();
        this.ChangeIconFontFamily();
        this.ChangeBackgroundTheme();
        this.RefreshBrushResourceDictionary();
        if (this.ColorResource != null)
            this.IsDark = this.ColorResource.IsDark;
    }

    public void RefreshBrushResourceDictionary()
    {
        ThemeTypeExtension.RefreshBrushResourceDictionary();
        if (this.BackgroundResource == null)
            return;
        this.BackgroundResource.Resource.RefreshResourceDictionary(); ;
    }


    private void ChangeColorTheme()
    {
        if (this.ColorResource == null)
            return;
        ResourceDictionary resource = this.ColorResource.Resource;
        ThemeTypeExtension.ChangeResourceDictionary(resource, x =>
        {
            return this.ColorResources.Any(l => l.Resource.Source == x.Source);
        });
    }

    private void ChangeBackgroundTheme()
    {
        if (this.BackgroundResource == null)
            return;
        ResourceDictionary resource = this.BackgroundResource.Resource;
        ThemeTypeExtension.ChangeResourceDictionary(resource, x =>
        {
            return this.BackgroundResources.Any(l => l.Resource.Source == x.Source);
        });
    }

    private void ChangeFontFamily()
    {
        Application.Current.Resources[SystemKeys.FontFamily] = this.FontFamily;
    }

    private void ChangeIconFontFamily()
    {
        Application.Current.Resources[SystemKeys.FontFamilyIcon] = this.IconFontFamily;
    }

    protected override string GetDefaultFolder()
    {
        return AppPaths.Instance.UserSetting;
    }
}
