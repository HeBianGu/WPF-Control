// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Encryption;
using H.Extensions.Setting;
using H.Services.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Windows.Markup;
using H.Services.Serializable;
using H.Services.Setting;
using H.Services.AppPath;

namespace H.Modules.License
{
    [Display(Name = "许可设置", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能设置许可验证方式")]
    internal class LicenseService : Settable<LicenseService>, ILicenseService
    {
        //public LicenseService()
        //{
        //    this.IsVisibleInSetting = false;
        //}

        protected override string GetDefaultFolder()
        {
            return AppPaths.Instance.License;
        }


        public override bool Save(out string message)
        {
            var r = this.SaveSystemPath(out message);
            if (!r) return false;
            r = this.SaveBaseDirectory(out message);
            return r;
        }

        bool SaveSystemPath(out string message)
        {
            return base.Save(out message);
        }

        bool SaveBaseDirectory(out string message)
        {
            message = null;
            string path = this.GetBaseDirectoryPath();
            this.GetSerializerService()?.Save(path, this);
            return true;
        }

        string GetBaseDirectoryPath()
        {
            return System.IO.Path.Combine(AppPaths.Instance.Config, "Microsoft.Extensions.Xmlable.dll");
            //return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Microsoft.Extensions.Xmlable.dll");
        }

        public override bool IsInit()
        {
            return !File.Exists(this.GetDefaultPath()) && !File.Exists(this.GetBaseDirectoryPath());
        }

        public override bool Load(out string message)
        {
            if (File.Exists(this.GetDefaultPath()))
            {
                base.Load(this.GetDefaultPath());
            }
            else if (File.Exists(this.GetBaseDirectoryPath()))
            {
                base.Load(this.GetBaseDirectoryPath());
            }

            return this.Save(out message);
        }

        private DateTime _trialEndTime = DateTime.Now.AddDays(30);
        [ReadOnly(true)]
        [Display(Name = "试用截止日期")]
        [ValueSerializer(typeof(DateTimeEncriyptoValueSerializer))]
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
            if (result == null) return null;
            //  Do ：验证成功，导入许可文件
            string file = this.GetLicFile(module);
            string folder = Path.GetDirectoryName(file);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            File.WriteAllText(file, lic);
            return result;
        }

        /// <summary>
        /// 检查本地许可是否合法
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public LicenseOption IsVail(out string error)
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
            string file = this.GetLicFile(module);
            if (!File.Exists(file))
            {
                error = "许可文件不存在";
                if (LicenseOptions.Instance.UseTrial)
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
                    return licenseOption;
                }

                return null;
            }
            string lic = File.ReadAllText(file);
            return this.IsVail(module, lic, out error);
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

        private string GetLicFile(string module)
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
