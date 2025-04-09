using H.Common.Attributes;
using H.Services.Message.Dialog.Commands;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Theme;

[Icon("\xE790")]
[Display(Name ="颜色主题", Description = "显示设置颜色主题")]
public class ShowColorThemeViewCommand : ShowIocPresenterCommandBase<IColorThemeViewPresenter>
{

}
