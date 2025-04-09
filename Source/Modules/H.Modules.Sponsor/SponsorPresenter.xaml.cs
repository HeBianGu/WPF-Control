// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Attributes;
using H.Mvvm.ViewModels.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace H.Modules.Sponsor;

[Icon("\xECC5")]
[Display(Name = "感谢对开源项目的赞助和支持", Description = "应用此功能给予赞助支持")]
internal class SponsorPresenter : DisplayBindableBase, ISponsorPresenter
{

}
