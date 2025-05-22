// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.Commands;
using H.Mvvm.Commands;
using H.Services.Common.Theme;
using Microsoft.Extensions.Options;

namespace H.Modules.Theme
{
    public class SwitchThemeViewPresenter : ISwitchThemeViewPresenter
    {
        private readonly IOptions<ThemeOptions> _options;
        public SwitchThemeViewPresenter(IOptions<ThemeOptions> options)
        {
            _options = options;
        }
        //public RelayCommand LoadedCommand => new RelayCommand(e=>
        //{
        //    this._options.Value.Refresh();
        //});

        public RelayCommand SwitchDarkCommand => new RelayCommand(x =>
        {
            this._options.Value.SwitchDark();
        });
    }
}
