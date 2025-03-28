﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Mvvm;
using H.Mvvm.ViewModels.Base;
using H.Services.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace H.Modules.Setting
{
    public class SettableGroup : BindableBase
    {
        public string Name { get; set; }
        private ObservableCollection<ISettable> _collection = new ObservableCollection<ISettable>();
        public ObservableCollection<ISettable> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        private ISettable _selectedSettable;
        public ISettable SelectedSettable
        {
            get { return _selectedSettable; }
            set
            {
                _selectedSettable = value;
                RaisePropertyChanged();
            }
        }
    }

    public class SettingViewPresenter : IocBindable<SettingViewPresenter, ISettingViewPresenter>, ISettingViewPresenter, ISettingDataManagerOption, IIconable
    {
        public SettingViewPresenter()
        {
            this.Groups = SettingDataManager.Instance.Settings?.GroupBy(l => l.GroupName).Select(x => new SettableGroup() { Name = x.Key, Collection = x.ToObservable() }).ToObservable();
            this.SelectedGroup = this.Groups?.FirstOrDefault();
            this.Title = "系统设置";
            this.Icon = "\xE115";
        }

        public string Title { get; set; }
        public string Icon { get; set; }

        private ObservableCollection<SettableGroup> _groups = new ObservableCollection<SettableGroup>();
        public ObservableCollection<SettableGroup> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                RaisePropertyChanged();
            }
        }

        private SettableGroup _selectedGroup;
        public SettableGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged();
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

        public void SwitchTo<T>() where T : ISettable
        {
            this.SwitchTo(typeof(T));
        }

        public void SwitchTo(Type type)
        {
            var find = this.Groups.SelectMany(x => x.Collection).FirstOrDefault(x => type.IsAssignableFrom(x.GetType()));
            if (find == null)
                return;
            this.SelectedGroup = this.Groups.FirstOrDefault(x => x.Collection.Contains(find));
            this.SelectedGroup.SelectedSettable = find;
        }
    }
}
