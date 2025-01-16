// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace H.Modules.Setting
{
    public class SettingViewPresenter : Ioc<SettingViewPresenter, ISettingViewPresenter>, ISettingViewPresenter, ISettingDataManagerOption
    {
        public SettingViewPresenter()
        {
            this.Data = SettingDataManager.Instance.Settings?.GroupBy(l => l.GroupName);
            this.Title = "系统设置";
        }

        public string Title { get; set; }

        private IEnumerable<IGrouping<string, ISettable>> _data;
        public IEnumerable<IGrouping<string, ISettable>> Data
        {
            get { return _data; }
            private set
            {
                _data = value;
            }
        }

        public void Add(params ISettable[] settings)
        {
            SettingDataManager.Instance.Add(settings);
        }

        public void Remove(params ISettable[] settings)
        {
            SettingDataManager.Instance.Remove(settings);
        }
    }
}
