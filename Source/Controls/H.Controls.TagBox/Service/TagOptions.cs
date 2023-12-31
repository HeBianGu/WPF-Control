using H.Extensions.Setting;
using H.Providers.Ioc;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Controls.TagBox
{
    [Display(Name = "标签管理", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息")]
    public class TagOptions : IocOptionInstance<TagOptions>
    {
        [Browsable(false)]
        public ObservableCollection<Tag> Tags { get; set; } = new ObservableCollection<Tag>();

        [ReadOnly(true)]
        [JsonIgnore]
        [XmlIgnore]
        public string SplitChars { get; set; } = ", \n \r";
    }
}
