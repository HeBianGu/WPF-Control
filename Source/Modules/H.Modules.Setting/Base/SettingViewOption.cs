// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Extensions.Setting;
using H.Services.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Modules.Setting
{
    [Display(Name = "设置页面", GroupName = SettingGroupNames.GroupControl)]
    internal class SettingViewOption : IocOptionInstance<SettingViewOption>, ISettingViewPresenterOption
    {
        private Thickness _margin = new Thickness(20);
        [DefaultValue(typeof(Thickness), "20,20,20,20")]
        [Display(Name = "页面边距")]
        public Thickness Margin
        {
            get { return _margin; }
            set
            {
                _margin = value;
                RaisePropertyChanged();
            }
        }

        private bool _usePassword;
        [DefaultValue(false)]
        [Display(Name = "启用密码")]
        public bool UsePassword
        {
            get { return _usePassword; }
            set
            {
                _usePassword = value;
            }
        }

        private double _titleWidth = 120.0;
        [DefaultValue(120.0)]
        [Display(Name = "标题宽度")]
        public double TitleWidth
        {
            get { return _titleWidth; }
            set
            {
                _titleWidth = value;
            }
        }

        private double _navigationTitleWidth = 120.0;
        [DefaultValue(120.0)]
        [Display(Name = "导航最小宽度")]
        public double NavigationiTitleWidth
        {
            get { return _navigationTitleWidth; }
            set
            {
                _titleWidth = value;
            }
        }

        private double _width = double.NaN;
        [TypeConverter(typeof(LengthConverter))]
        [DefaultValue(double.NaN)]
        [Display(Name = "页面宽度")]
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
            }
        }

        private double _minWidth = 100;
        [TypeConverter(typeof(LengthConverter))]
        [DefaultValue(100)]
        [Display(Name = "最小宽度")]
        public double MinWidth
        {
            get { return _minWidth; }
            set
            {
                _minWidth = value;
            }
        }

        private double _height = double.NaN;
        [DefaultValue(double.NaN)]
        [TypeConverter(typeof(LengthConverter))]
        [Display(Name = "页面高度")]
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
            }
        }

        private double _minHeight = 100;
        [DefaultValue(100)]
        [TypeConverter(typeof(LengthConverter))]
        [Display(Name = "最小高度")]
        public double MinHeight
        {
            get { return _minHeight; }
            set
            {
                _minHeight = value;
            }
        }
    }
}
