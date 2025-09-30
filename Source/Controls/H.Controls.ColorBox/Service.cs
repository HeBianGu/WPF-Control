// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Setting;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace H.Controls.ColorBox
{
    public class ColorBoxService : IColorBoxService
    {

    }

    public interface IColorBoxService
    {

    }

    [Display(Name = "颜色列表设置", GroupName = SettingGroupNames.GroupControl)]
    public class ColorBoxSetting : Settable<ColorBoxSetting>
    {
        public ColorBoxSetting()
        {
            //this.IsVisibleInSetting = false;
        }
        private int _from = 0;
        [Display(Name = "起始位置")]
        [DefaultValue(0)]
        [Browsable(false)]
        public int From
        {
            get { return _from; }
            set
            {
                _from = value;
                RaisePropertyChanged();
            }
        }

        private int _to = 360;
        [Display(Name = "结束位置")]
        [DefaultValue(360)]
        [Browsable(false)]
        public int To
        {
            get { return _to; }
            set
            {
                _to = value;
                RaisePropertyChanged();
            }
        }

        private int _step = 48;
        [Display(Name = "步进")]
        [DefaultValue(48)]
        [Browsable(false)]
        public int Step
        {
            get { return _step; }
            set
            {
                _step = value;
                RaisePropertyChanged();
            }
        }

        private DoubleCollection _depthes = new DoubleCollection(new double[] { 1.0, 0.8, 0.6, 0.4, 0.2 });
        [Browsable(false)]
        [Display(Name = "颜色深度")]
        public DoubleCollection Depthes
        {
            get { return _depthes; }
            set
            {
                _depthes = value;
                RaisePropertyChanged();
            }
        }
    }
}
