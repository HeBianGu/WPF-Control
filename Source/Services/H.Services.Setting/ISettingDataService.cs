// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Setting;

public interface ISettingDataService : ISettingDataOption
{
    ObservableCollection<ISettable> Settings { get; set; }
    void Cancel();
    bool Load(Action<ISettable> action, out string message);
    bool LoadLoginedLoad(Action<ISettable> action, out string message);
    void Remove(params ISettable[] settings);
    bool Save(out string message);
    void SetDefault();

    void Clear();
}
