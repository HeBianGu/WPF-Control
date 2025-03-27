// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Iocable;
using H.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.DrawerBox
{
    [Icon("\xE713")]
    [Display(Name = "隐藏", Description = "隐藏页面")]
    public class CloseDrawerCommand : DisplayMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is DrawerBox drawer)
                drawer.Close();
        }
    }

}
