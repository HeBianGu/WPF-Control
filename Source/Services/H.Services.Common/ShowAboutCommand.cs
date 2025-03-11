using H.Mvvm.Attributes;
using System.ComponentModel.DataAnnotations;

namespace H.Services.Common
{
    [Icon("\xE946")]
    [Display(Name = "关于", Description = "显示关于页面")]
    public class ShowAboutCommand : ShowIocCommandBase<IAboutViewPresenter>
    {

    }
}
