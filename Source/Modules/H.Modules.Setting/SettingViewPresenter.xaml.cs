// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Providers.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace H.Modules.Setting
{
    public class SettingViewPresenter : Ioc<SettingViewPresenter, ISettingViewPresenter>, ISettingViewPresenter, ISettingViewPresenterOption
    {
        public SettingViewPresenter()
        {
            this.Data = SettingDataManager.Instance.Settings?.GroupBy(l => l.GroupName);
        }

        private IEnumerable<IGrouping<string, ISetting>> _data;
        public IEnumerable<IGrouping<string, ISetting>> Data
        {
            get { return _data; }
            private set
            {
                _data = value;
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

        private double _titleWidth = double.NaN;
        [DefaultValue(double.NaN)]
        [Display(Name = "标题宽度")]
        public double TitleWidth
        {
            get { return _titleWidth; }
            set
            {
                _titleWidth = value;
            }
        }

        public void Add(params ISetting[] settings)
        {
            SettingDataManager.Instance.Add(settings);
        }

        public void Remove(params ISetting[] settings)
        {
            SettingDataManager.Instance.Remove(settings);
        }
    }

    public class SettingButtonPresenter : ISettingButtonPresenter
    {

    }
}
