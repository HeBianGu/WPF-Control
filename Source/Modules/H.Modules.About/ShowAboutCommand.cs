using H.Common.Attributes;
using H.Services.Common.About;
using H.Services.Message.Dialog.Commands;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.About;

[Icon("\xE946")]
[Display(Name = "关于", Description = "显示关于页面")]
public class ShowAboutCommand : ShowIocPresenterCommandBase<IAboutViewPresenter>
{

}