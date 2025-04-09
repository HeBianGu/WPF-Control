using H.Common.Attributes;
using H.Common.Interfaces;
using H.Extensions.FontIcon;
using H.Services.Common.About;
using H.Services.Setting;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Serialization;

namespace H.Modules.About
{

    [Icon(FontIcons.Info)]
    [Display(Name = "关于", GroupName = SettingGroupNames.GroupSystem, Description = "这是一个关于页面的信息")]
    public class AboutViewPresenter : Ioc<AboutViewPresenter, IAboutViewPresenter>, IAboutViewPresenter
    {

    }
}
