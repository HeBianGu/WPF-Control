// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Controls.Form;
using H.Extensions.Setting;
using H.Services.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using H.Controls.Form.PropertyItem.TextPropertyItems;

namespace H.Modules.Setting
{
    [Display(Name = "设置页面", GroupName = SettingGroupNames.GroupControl)]
    internal class SettingViewOption : IocOptionInstance<SettingViewOption>, ISettingViewPresenterOption
    {
        private Thickness _margin = new Thickness(20);
        [DefaultValue(typeof(Thickness), "20,20,20,20")]
        [Display(Name = "页面边距", Description = "设置页面边距")]
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
        [Display(Name = "启用密码", Description = "设置是否启用密码，启用后进入设置页面需要输入预设管理员密码")]
        public bool UsePassword
        {
            get { return _usePassword; }
            set
            {
                _usePassword = value;
            }
        }

        private bool _rememberPassword;
        [DefaultValue(false)]
        [Display(Name = "记住密码", Description = "记住密码后下次打开设置页面自动进入")]
        public bool RememberPassword
        {
            get { return _rememberPassword; }
            set
            {
                _rememberPassword = value;
            }
        }

        private string _password;
        [PropertyItem(typeof(PasswordTextPropertyItem))]
        [DefaultValue("123456")]
        [Display(Name = "设置管理员密码", Description = "设置是否启用密码，启用后进入设置页面需要输入预设管理员密码")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }

        private int _passwordCount;
        [DefaultValue(3)]
        [Display(Name = "密码校验次数", Description = "密码输入错误次数超过密码校验次数不许再输入")]
        public int PasswordCount
        {
            get { return _passwordCount; }
            set
            {
                _passwordCount = value;
            }
        }

        private double _titleWidth = 120.0;
        [DefaultValue(120.0)]
        [Display(Name = "标题宽度", Description = "设置页面标题宽度")]
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
        [Display(Name = "导航最小宽度", Description = "设置页面导航最小宽度")]
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
        [Display(Name = "页面宽度", Description = "设置页面宽度")]
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
        [Display(Name = "最小宽度", Description = "设置页面最小宽度")]
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
        [Display(Name = "页面高度", Description = "设置页面高度")]
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
        [Display(Name = "最小高度", Description = "设置页面最小高度")]
        public double MinHeight
        {
            get { return _minHeight; }
            set
            {
                _minHeight = value;
            }
        }
    }

    [Display(Name = "设置页面控制", GroupName = SettingGroupNames.GroupSecurity)]
    public class SettingSecurityViewOption : IocOptionInstance<SettingSecurityViewOption>
    {
        private bool _usePassword;
        [DefaultValue(false)]
        [Display(Name = "启用密码", Description = "设置是否启用密码，启用后进入设置页面需要输入预设管理员密码")]
        public bool UsePassword
        {
            get { return _usePassword; }
            set
            {
                _usePassword = value;
            }
        }

        private bool _rememberPassword;
        [DefaultValue(true)]
        [Display(Name = "记住密码", Description = "记住密码后下次打开设置页面自动进入")]
        public bool RememberPassword
        {
            get { return _rememberPassword; }
            set
            {
                _rememberPassword = value;
            }
        }

        private string _password;
        [PropertyItem(typeof(PasswordTextPropertyItem))]
        [DefaultValue("123456")]
        [Display(Name = "设置管理员密码", Description = "设置是否启用密码，启用后进入设置页面需要输入预设管理员密码")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }

        private int _passwordCount;
        [DefaultValue(3)]
        [Display(Name = "密码校验次数", Description = "密码输入错误次数超过密码校验次数不许再输入")]
        public int PasswordCount
        {
            get { return _passwordCount; }
            set
            {
                _passwordCount = value;
            }
        }
    }
}
