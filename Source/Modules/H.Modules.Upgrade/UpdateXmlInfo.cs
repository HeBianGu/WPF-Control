using System.Xml.Serialization;

namespace H.Modules.Upgrade;

[XmlRoot("item")]
public class UpdateXmlInfo
{
    [XmlElement("url")]
    public string Url { get; set; }

    [XmlElement("changelog")]
    public string Changelog { get; set; }

    [XmlElement("version")]
    public string Version { get; set; }

    /// <summary>
    /// 是否强制更新，是则不更新程序推出
    /// </summary>
    [XmlElement("force")]
    public bool Force { get; set; } = false;
}