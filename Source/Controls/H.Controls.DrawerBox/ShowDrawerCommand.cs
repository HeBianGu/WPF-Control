// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Common.Attributes;
global using H.Common.Commands;
global using H.Iocable;
global using H.Mvvm;
global using System.ComponentModel.DataAnnotations;

namespace H.Controls.DrawerBox
{
    [Icon("\xE713")]
    [Display(Name = "显示", Description = "显示页面")]
    public class ShowDrawerCommand : DisplayMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is DrawerBox drawer)
                drawer.Show();
        }
    }

}
