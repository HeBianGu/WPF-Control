// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;

namespace H.Extensions.DataBase.Repository
{
    public class RepositoryBindableBase : CommandsBindableBase
    {
        //public RepositoryBindableBase()
        //{
        //    System.Collections.Generic.IEnumerable<PropertyInfo> cmdps = GetType().GetProperties().Where(x => typeof(ICommand).IsAssignableFrom(x.PropertyType));
        //    foreach (PropertyInfo cmdp in cmdps)
        //    {
        //        if (cmdp.CanRead == false)
        //            continue;
        //        if (cmdp.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
        //            continue;
        //        ICommand command = cmdp.GetValue(this) as ICommand;
        //        if (command is IRelayCommand relay)
        //        {
        //            if (relay.Name == null || relay.GroupName == null)
        //            {
        //                var display = cmdp.GetCustomAttribute<DisplayAttribute>();
        //                relay.Name = relay.Name ?? display?.Name ?? cmdp.Name;
        //                relay.GroupName = relay.GroupName ?? display?.GroupName;
        //            }
        //        }
        //        this.Commands.Add(command);
        //    }
        //}

        //[Browsable(false)]
        //[System.Text.Json.Serialization.JsonIgnore]

        //[System.Xml.Serialization.XmlIgnore]
        //public ObservableCollection<ICommand> Commands { get; } = new ObservableCollection<ICommand>();

    }
}
