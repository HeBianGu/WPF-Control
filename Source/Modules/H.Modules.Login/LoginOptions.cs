// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.Common;
using H.Extensions.FontIcon;
using H.Extensions.Setting;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Modules.Login
{
    [Icon(FontIcons.Connect)]
    [Display(Name = "登录页面设置", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息")]
    public class LoginOptions : IocOptionInstance<LoginOptions>, ILoginOptions
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.Product = ApplicationProvider.Product;
        }

        public override bool Load(out string message)
        {
            return base.Load(out message);
        }

        public override bool Save(out string message)
        {
            return base.Save(out message);
        }

        private string _product;
        [Display(Name = "登录标题")]
        public string Product
        {
            get { return _product; }
            set
            {
                _product = value;
                RaisePropertyChanged();
            }
        }

        private double _productFontSize;
        [DefaultValue(50)]
        [Display(Name = "字体大小")]
        public double ProductFontSize
        {
            get { return _productFontSize; }
            set
            {
                _productFontSize = value;
                RaisePropertyChanged();
            }
        }
        private string _adminName;
        [DefaultValue("admin")]
        [ReadOnly(true)]
        [Required]
        [Display(Name = "管理员账号")]
        public string AdminName
        {
            get { return _adminName; }
            set
            {
                _adminName = value;
                RaisePropertyChanged();
            }
        }

        private string _adminPassword;
        [JsonIgnore]

        [XmlIgnore]
        [Browsable(false)]
        [DefaultValue("123456")]
        [Required]
        [Display(Name = "管理员密码")]
        public string AdminPassword
        {
            get { return _adminPassword; }
            set
            {
                _adminPassword = value;
                RaisePropertyChanged();
            }
        }

        private string _lastUserName;
        [ReadOnly(true)]
        [DefaultValue("admin")]
        [Display(Name = "上次登录用户")]
        public string LastUserName
        {
            get { return _lastUserName; }
            set
            {
                _lastUserName = value;
                RaisePropertyChanged();
            }
        }

        private string _lastPassword;
        [Browsable(false)]
        [DefaultValue("123456")]
        [Display(Name = "上次登录密码")]
        public string LastPassword
        {
            get { return _lastPassword; }
            set
            {
                _lastPassword = value;
                RaisePropertyChanged();
            }
        }

        private bool _remember = true;
        [Display(Name = "记住密码")]
        public bool Remember
        {
            get { return _remember; }
            set
            {
                _remember = value;
                RaisePropertyChanged();
            }
        }

        private bool _useVisitor = false;
        [DefaultValue(false)]
        [Display(Name = "启用访客", Description = "启用访客模式后登录不成功也可以进入主窗口")]
        public bool UseVisitor
        {
            get { return _useVisitor; }
            set
            {
                _useVisitor = value;
                RaisePropertyChanged();
            }
        }

        private bool _useLogoutRestart = false;
        [DefaultValue(false)]
        [Display(Name = "退出登录重启", Description = "启用退出登录重启应用")]
        public bool UseLogoutRestart
        {
            get { return _useLogoutRestart; }
            set
            {
                _useLogoutRestart = value;
                RaisePropertyChanged();
            }
        }

    }
}
