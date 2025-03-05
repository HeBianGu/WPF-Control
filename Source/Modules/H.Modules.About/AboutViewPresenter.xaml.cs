using H.Extensions.FontIcon;
using H.Mvvm;
using H.Mvvm.Attributes;
using H.Services.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Xml.Serialization;

namespace H.Modules.About
{

    [Icon(FontIcons.Info)]
    [Display(Name = "关于", GroupName = SettingGroupNames.GroupSystem, Description = "这是一个关于页面的信息")]
    public class AboutViewPresenter : Ioc<AboutViewPresenter, IAboutViewPresenter>, IAboutViewPresenter, IAboutViewPresenterOption, IIconable
    {
        public AboutViewPresenter()
        {
            this._ProductName = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
            this._productDescription = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;
            this._company = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
            this._culture = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCultureAttribute>()?.Culture;
            this._trademark = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTrademarkAttribute>()?.Trademark;
            this._configuration = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration;
            this._copyright = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright;
            this._version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyVersionAttribute>()?.Version;
            this._fileVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version;
        }
        public string Title { get; set; } = "关于";
        public string Icon { get; set; } = FontIcons.Info;

        private string _ProductName;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "产品名称")]
        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                _ProductName = value;
            }
        }

        private string _productDescription;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "产品信息")]
        public string ProductDescription
        {
            get { return _productDescription; }
            set
            {
                _productDescription = value;
            }
        }

        private string _company;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "公司信息")]
        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
            }
        }


        private string _culture;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "产品区域")]
        public string Culture
        {
            get { return _culture; }
            set
            {
                _culture = value;
            }
        }

        private string _trademark;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "产品商标")]
        public string Trademark
        {
            get { return _trademark; }
            set
            {
                _trademark = value;
            }
        }

        private string _configuration;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "产品配置")]
        public string Configuration
        {
            get { return _configuration; }
            set
            {
                _configuration = value;
            }
        }

        private string _privacy;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [ReadOnly(true)]
        [DefaultValue("https://github.com/HeBianGu/WPF-Control")]
        [Display(Name = "隐私政策")]
        public string Privacy
        {
            get { return _privacy; }
            set
            {
                _privacy = value;
            }
        }

        private string _agreement;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [ReadOnly(true)]
        [DefaultValue("https://github.com/HeBianGu/WPF-Control")]
        [Display(Name = "服务协议")]
        public string Agreement
        {
            get { return _agreement; }
            set
            {
                _agreement = value;
            }
        }

        private string _copyright;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "许可证书")]
        public string Copyright
        {
            get { return _copyright; }
            set
            {
                _copyright = value;
            }
        }

        private string _version;
        [ReadOnly(true)]
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [Display(Name = "产品版本")]
        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
            }
        }

        private string _fileVersion;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "文件版本")]
        public string FileVersion
        {
            get { return _fileVersion; }
            set
            {
                _fileVersion = value;
            }
        }

        private string _webSet;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [ReadOnly(true)]
        [DefaultValue("https://github.com/HeBianGu/WPF-Control")]
        [Display(Name = "官方网站")]
        public string WebSet
        {
            get { return _webSet; }
            set
            {
                _webSet = value;
            }
        }

        private string _contact;
        [System.Text.Json.Serialization.JsonIgnore]

        [System.Xml.Serialization.XmlIgnore]
        [ReadOnly(true)]
        [DefaultValue("QQ:908293466")]
        [Display(Name = "联系方式")]
        public string Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
            }
        }
    }
}
