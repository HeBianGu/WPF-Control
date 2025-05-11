// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Setting;

namespace H.Controls.PrintBox
{
    [Display(Name = "打印设置", GroupName = SettingGroupNames.GroupControl)]
    public class PrintBoxSetting : Settable<PrintBoxSetting>
    {
        private double _printableAreaWidth;
        public double PrintableAreaWidth
        {
            get { return _printableAreaWidth; }
            set
            {
                _printableAreaWidth = value;
                RaisePropertyChanged();
            }
        }
    }
}
