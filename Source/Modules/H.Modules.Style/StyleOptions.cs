using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using H.Extensions.Setting;
using H.Services.AppPath;
using H.Services.Setting;
using H.Styles.Bootstrap.StyleResources;
using H.Themes.Colors;
using H.Themes.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Serialization;
using H.Styles.StyleResources;

namespace H.Modules.Style;

[Display(Name = "控件样式设置", GroupName = SettingGroupNames.GroupStyle, Description = "控件样式设置设置的信息")]
public class StyleOptions : IocOptionInstance<StyleOptions>, IStyleOptions
{
    public StyleOptions()
    {
        this.StyleResources.Add(new ConciseStyleResource());
        this.StyleResources.Add(new NoneStyleResource());
        this.StyleResources.Add(new BootstrapStyleResource());
    }

    private IStyleResource _styleResource;
    [JsonIgnore]
    [XmlIgnore]
    [PropertyNameSourcePropertyItem(typeof(ComboBoxPropertyItem), nameof(StyleResources))]
    [Display(Name = "控件样式")]
    public IStyleResource StyleResource
    {
        get { return _styleResource; }
        set
        {
            _styleResource = value;
            RaisePropertyChanged();
            this.Refresh();
        }
    }
    [Browsable(false)]
    public int StyleResourceSelectedIndex { get; set; }
    [JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public List<IStyleResource> StyleResources { get; set; } = new List<IStyleResource>();

    internal void Refresh()
    {
        this.StyleResource.Resource.ChangeResourceDictionary(x =>
        {
            return this.StyleResources.Any(l => l.Resource.Source == x.Source);
        });
        this.Save(out string message);
    }

    public override bool Save(out string message)
    {
        this.StyleResourceSelectedIndex = this.StyleResources.IndexOf(this.StyleResource);
        return base.Save(out message);
    }

    public override bool Load(out string message)
    {
        var r = base.Load(out message);
        this.StyleResource = this.StyleResources[this.StyleResourceSelectedIndex];
        this.Refresh();
        return r;
    }
}
