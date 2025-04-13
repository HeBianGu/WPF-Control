
using H.Mvvm;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using H.Mvvm.ViewModels.Base;
using H.Mvvm.Commands;
using H.Services.Message.Dialog;
using H.Services.Identity;
using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Login
{
    [Icon(FontIcons.Contact)]
    [Display(Name = "当前用户", GroupName = SettingGroupNames.GroupSystem, Description = "当前登录的用户信息")]
    public class CurrentUserViewPresenter : DisplayBindableBase, ICurrentUserViewPresenter
    {
  
    }

    public interface ICurrentUserViewPresenter
    {

    }
}
