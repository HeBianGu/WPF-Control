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
using H.Extensions.Mvvm.ViewModels.Base;
using H.Modules.Plugin.Base;
using H.Services.Setting;
using Microsoft.Extensions.Options;

namespace H.Modules.Plugin.Presenter;

[Icon(FontIcons.Component)]
[Display(Name = "插件管理", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能对插件管理")]
public class PluginManagerPresenter : DisplayBindableBase, IPluginManagerPresenter
{
    private readonly IOptions<PluginOptions> _options;
    public PluginManagerPresenter(IOptions<PluginOptions> options)
    {
        _options = options;
        this.Load();
    }

    private ObservableCollection<PluginData> _PluginDatas = new ObservableCollection<PluginData>();
    public ObservableCollection<PluginData> PluginDatas
    {
        get { return _PluginDatas; }
        set
        {
            _PluginDatas = value;
            RaisePropertyChanged();
        }
    }

    public void Load()
    {
        this.PluginDatas = this.GetPluginDatas().ToObservable();
    }

    private IEnumerable<PluginData> GetPluginDatas()
    {
        foreach (var item in PluginManager.GetPluginAssemblies())
        {
            var attribute = item.GetCustomAttribute<PluginAttribute>();
            if (attribute != null)
            {
                var aname = item.GetName();
                yield return new PluginData()
                {
                    Name = attribute.Name,
                    GroupName = attribute.GroupName,
                    Description = attribute.Description,
                    DllPath = item.Location.GetAppDomainRelativePath(),
                    FullName = aname.FullName,
                    DateTime = item.Location.ToFileEx().GetLastAccessTime()?.ToString("yyyy-MM-dd HH:mm:ss"),
                    FileSize = item.Location.ToFileEx().GetFileSizeToDisplay()
                };
            }
        }
    }
}
