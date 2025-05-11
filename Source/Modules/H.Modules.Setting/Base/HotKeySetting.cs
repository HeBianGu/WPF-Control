// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Setting;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Xml.Serialization;

namespace H.Modules.Setting.Base;

/// <summary> 热键</summary>
[Display(Name = "热键", GroupName = SettingGroupNames.GroupBase)]
public class HotKeySetting : Settable<HotKeySetting>
{
    private ObservableCollection<HotKeyItem> _hotKeys = new ObservableCollection<HotKeyItem>();
    /// <summary> 说明  </summary>
    public ObservableCollection<HotKeyItem> HotKeys
    {
        get { return _hotKeys; }
        set
        {
            _hotKeys = value;
            RaisePropertyChanged("HotKeys");
        }
    }
}

public class HotKeyItem
{
    public ModifierKeys ModifierKeys { get; set; }

    public Key Key { get; set; }

    public string DisplayName { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]

    [XmlIgnore]
    public ICommand Command { get; set; }
}

//public class HotKeyService : LazyInstance<HotKeyService>
//{
//    public HotKeySetting GetHotKey(object obj)
//    {
//        HotKeySetting result = new HotKeySetting();

//        System.Collections.Generic.IEnumerable<PropertyInfo> cmds = obj.GetType().GetProperties().Where(x => typeof(ICommand).IsAssignableFrom(x.PropertyType));

//        foreach (PropertyInfo cmd in cmds)
//        {
//            BrowsableAttribute browsable = cmd.GetCustomAttribute<BrowsableAttribute>();

//            if (browsable?.Browsable != true) continue;

//            DisplayAttribute display = cmd.GetCustomAttribute<DisplayAttribute>();

//            HotKeyAttribute hotKey = cmd.GetCustomAttribute<HotKeyAttribute>();

//            if (hotKey == null) continue;

//            HotKeyItem hotKeyItem = new HotKeyItem();

//            hotKeyItem.Command = cmd.GetValue(obj) as ICommand;

//            hotKeyItem.Key = hotKey.Key;

//            hotKeyItem.ModifierKeys = hotKey.ModifierKeys;

//            hotKeyItem.DisplayName = hotKey.DisplayName ?? display?.Name;

//            result.HotKeys.Add(hotKeyItem);
//        }

//        return result;
//    }

//}
