﻿using H.Mvvm;
using H.Mvvm.Attributes;
using System.ComponentModel.DataAnnotations;

namespace H.Services.Common
{
    [Icon("\xE963")]
    [Display(Name = "新手向导", Description = "显示新手向导")]
    public class ShowGuideCommand : DisplayMarkupCommandBase
    {
        public ShowGuideCommand()
        {
            this.Icon = "\xE963";
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await Ioc<IGuideService>.Instance.Show();
        }
    }

}
