// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Common.Interfaces;
using H.Extensions.Common;
using H.Mvvm.ViewModels.Base;
using System.Collections.ObjectModel;

namespace H.Modules.Setting;

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

public class SettingViewPresenter : IocBindable<SettingViewPresenter, ISettingViewPresenter>, ISettingViewPresenter, IIconable
{
    public SettingViewPresenter()
    {
        this.RefreshSettingData();
        this.Title = "系统设置";
        this.Icon = "\xE115";
    }

    public void RefreshSettingData()
    {
        this.Groups = IocSetting.Instance.Settings?.Where(x => x.IsVisibleInSetting).GroupBy(l => l.GroupName).Select(x => new SettableGroup() { Name = x.Key, Collection = x.ToObservable() }).ToObservable();
        this.SelectedGroup = this.Groups?.FirstOrDefault();
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
        IocSetting.Instance.Add(settings);
    }

    public void Remove(params ISettable[] settings)
    {
        IocSetting.Instance.Remove(settings);
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

    public async Task<bool> Show(Type switchType)
    {
        return await this.Show(switchType, null);
    }
    public async Task<bool> Show(Type switchType, Action<IDialog> builder = null)
    {
        var setting = Ioc.GetService<ISettingViewPresenter>();
        if (switchType != null)
            setting.SwitchTo(switchType);
        bool? r = await IocMessage.Dialog.Show(setting, x =>
        {
            builder?.Invoke(x);
            x.Width = SettingViewOptions.Instance.Width;
            x.Height = SettingViewOptions.Instance.Height;
            x.Margin = SettingViewOptions.Instance.Margin;
            x.MinWidth = SettingViewOptions.Instance.MinWidth;
            x.MinHeight = SettingViewOptions.Instance.MinHeight;
            x.HorizontalAlignment = HorizontalAlignment.Stretch;
            x.VerticalAlignment = VerticalAlignment.Stretch;
            x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            x.VerticalContentAlignment = VerticalAlignment.Stretch;
            x.DialogButton = DialogButton.None;
            if (x is Window window)
            {
                window.SizeToContent = SizeToContent.Manual;
                window.ResizeMode = ResizeMode.CanResize;
                window.ShowInTaskbar = true;
                window.VerticalContentAlignment = VerticalAlignment.Stretch;
            }
        });
        if (r != true)
            return false;
        bool sr = IocSetting.Instance.Save(out string error);
        if (sr == false)
            await IocMessage.Dialog.Show(error);
        return sr;
    }
}
