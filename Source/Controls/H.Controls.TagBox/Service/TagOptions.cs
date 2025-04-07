global using H.Extensions.Setting;
global using H.Services.Setting;
global using System.Collections.ObjectModel;

namespace H.Controls.TagBox
{
    [Display(Name = "标签管理", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息")]
    public class TagOptions : IocOptionInstance<TagOptions>, ITagOptions
    {
        [Browsable(false)]
        public ObservableCollection<Tag> Tags { get; set; } = new ObservableCollection<Tag>();

        [ReadOnly(true)]
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        public string SplitChars { get; set; } = ", \n \r";
    }
}
