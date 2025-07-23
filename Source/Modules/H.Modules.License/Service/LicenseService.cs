// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Extensions.Setting;
using H.Services.AppPath;
using H.Services.Serializable;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Windows.Markup;

namespace H.Modules.License
{

    [Display(Name = "许可设置", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能设置许可验证方式")]
    public class LicenseService : Settable<LicenseService>, ILicenseService
    {
        public LicenseService()
        {
            this.UseTrial = LicenseOptions.Instance.UseTrial;
            this.TrialEndTime = DateTime.Now.AddDays(LicenseOptions.Instance.TrialDays);
        }

        protected override string GetDefaultFolder()
        {
            return AppPaths.Instance.License;
        }

        private bool _UseTrial;
        [ReadOnly(true)]
        [Display(Name = "启用试用")]
        public bool UseTrial
        {
            get { return _UseTrial; }
            set
            {
                _UseTrial = value;
                RaisePropertyChanged();
            }
        }


        private DateTime _trialEndTime = DateTime.Now.AddDays(30);
        [ReadOnly(true)]
        [Display(Name = "试用截止日期")]
        //[TypeConverter(typeof(DateTimeEncriyptoConverter))]
        //[JsonConverter(typeof(EncriyptoDateTimeJsonConverter))]
        public DateTime TrialEndTime
        {
            get { return _trialEndTime; }
            set
            {
                _trialEndTime = value;
                RaisePropertyChanged();
            }
        }


        public LicenseOption TryActive(string module, string lic, out string error)
        {
            LicenseOption result = this.IsVail(module, lic, out error);
            if (result == null)
                return null;
            this.OnActiveLic(module, lic);
            return result;
        }

        protected virtual void OnActiveLic(string module, string lic)
        {
            string file = this.GetLicFile(module);
            string folder = Path.GetDirectoryName(file);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            File.WriteAllText(file, lic);
        }

        /// <summary>
        /// 检查本地许可是否合法
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public virtual LicenseOption IsVail(out string error)
        {
            string module = Assembly.GetEntryAssembly().GetName().Name;
            return this.IsVail(module, out error);
        }

        /// <summary>
        /// 检查本地许可是否合法
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public LicenseOption IsVail(string module, out string error)
        {
            module = module ?? Assembly.GetEntryAssembly().GetName().Name;
            string file = this.GetLicFile(module);
            if (!File.Exists(file))
            {
                error = "许可文件不存在";
                if (this.UseTrial)
                {
                    if (this.TrialEndTime.Date < DateTime.Now.Date)
                    {
                        error = "试用许可已到期，请申请正式许可";
                        return null;
                    }
                    LicenseOption licenseOption = new LicenseOption();
                    licenseOption.EndTime = this.TrialEndTime;
                    licenseOption.HostID = this.GetHostID();
                    licenseOption.Module = module;
                    licenseOption.Level = -1;
                    this.OnTrial();
                    return licenseOption;
                }

                return null;
            }
            string lic = File.ReadAllText(file);
            return this.IsVail(module, lic, out error);
        }

        protected virtual void OnTrial()
        {

        }

        /// <summary>
        /// 检验许可文本是否可用
        /// </summary>
        /// <param name="lic"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private LicenseOption IsVail(string module, string lic, out string error)
        {
            string value = RSAHelper.DecryptString(lic, this.GetPublicKey());
            LicenseOption licenseOption = new LicenseOption();
            licenseOption.HostID = this.GetHostID();
            licenseOption.Module = module;
            if (!licenseOption.IsMatch(value, out error))
            {
                licenseOption.License = lic;
                return null;
            }
            licenseOption.License = lic;
            return licenseOption;
        }

        protected virtual string GetLicFile(string module)
        {
            return System.IO.Path.Combine(LicenseOptions.Instance.FilePath, module, "license.lic");
        }

        private string GetPublicKey()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "default.pub");
            return File.ReadAllText(path);
        }

        public string GetHostID()
        {
            return SystemInfo.Instance.HostID;
        }
    }
}
