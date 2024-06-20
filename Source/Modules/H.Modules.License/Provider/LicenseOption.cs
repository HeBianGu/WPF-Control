// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Globalization;

namespace H.Modules.License
{
    public class LicenseOption
    {
        public string HostID { get; set; }
        public string License { get; set; }
        public string Module { get; set; }
        /// <summary>
        /// -1表示试用
        /// </summary>
        public int Level { get; set; }
        public DateTime EndTime { get; set; }
        public override string ToString()
        {
            return (this.HostID + "|" + this.EndTime.ToString("yyyy-MM-dd HH:mm:ss") + "|" + this.Module + "|" + this.Level);
        }

        public bool IsMatch(string str, out string error)
        {
            string[] ss = str.Split(new char[] { '|' });
            if (ss.Length != 4)
            {
                error = "不是有效注册码";
                return false;
            }
            if (ss[0] != this.HostID)
            {
                error = "HostID不匹配";
                return false;
            }
            if ((ss[2] ?? string.Empty) != (this.Module ?? string.Empty))
            {
                error = "模块不匹配";
                return false;
            }
            DateTime time = DateTime.ParseExact(ss[1], "yyyy-MM-dd HH:mm:ss", new CultureInfo("zh-CN"));
            if (time.Date < DateTime.Now.Date)
            {
                error = $"许可已过期[{time.Date}]";
                return false;
            }
            error = null;
            this.EndTime = time;
            this.License = str;
            if (int.TryParse(ss[3], out int l))
            {
                this.Level = l;
            }
            return true;
        }

        public string GetDisplay()
        {
            if (this.EndTime.Date == DateTime.MaxValue.Date) return "永久";

            return this.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}
