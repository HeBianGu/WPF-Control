// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.Mvvm.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Sponsor;

[Icon("\xECC5")]
[Display(Name = "感谢对开源项目的赞助和支持", Description = "应用此功能给予赞助支持")]
internal class SponsorPresenter : DisplayBindableBase, ISponsorPresenter
{

}
