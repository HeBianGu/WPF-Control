// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Extensions.Setting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Serialization;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Controls.Form.Attributes;

namespace H.Modules.Help.Base;

public class UriHelpOptionsBase<T> : IocOptionInstance<T>, IUriHelpOptions where T : class, new()
{
    private string _uri;
    [System.Text.Json.Serialization.JsonIgnore]
    [XmlIgnore]
    [ReadOnly(true)]
    [PropertyItem(typeof(HyperlinkPropertyItem))]
    [Display(Name = "地址")]
    public string Uri
    {
        get { return _uri; }
        set
        {
            _uri = value;
            RaisePropertyChanged();
        }
    }
}

