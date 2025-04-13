using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Message;
using H.Services.Message.Dialog;
using H.Services.Message.Dialog.Commands;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Login.Commands;

[Icon(FontIcons.View)]
[Display(Name = "用户信息", GroupName = SettingGroupNames.GroupSystem, Description = "显示当前用户信息")]
public class ShowCurrentUserCommand : ShowIocPresenterCommandBase<ICurrentUserViewPresenter>
{

}