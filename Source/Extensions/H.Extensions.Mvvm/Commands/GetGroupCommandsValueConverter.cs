// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.ValueConverter;
using System.Collections.Generic;

namespace H.Extensions.Mvvm.Commands;

public class GetGroupCommandsValueConverter : MarkupValueConverterBase
{
    public string GroupName { get; set; } = CommandGroupNames.MenuBar;
    public string ExceptGroupName { get; set; }
    public bool UseSeparator { get; set; } = true;
    public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is ICommandsBindable bindable)
            return this.CreateSeparatorCommands(bindable.Commands);
        if (value is IEnumerable<ICommand> commands)
            return this.CreateSeparatorCommands(commands);
        return DependencyProperty.UnsetValue;
    }

    protected virtual IEnumerable<IDisplayCommand> CreateCommands(IEnumerable<ICommand> commands)
    {
        return commands.GetGroupCommands(this.GroupName).GetExceptGroupCommands(this.ExceptGroupName);
    }

    protected virtual IEnumerable<object> CreateSeparatorCommands(IEnumerable<ICommand> commands)
    {
        var displayCommands = this.CreateCommands(commands);
        var groupedCommands = displayCommands.GroupBy(x => x.GroupName).ToList();
        if (groupedCommands.Count() <= 1)
        {
            foreach (var item in displayCommands)
            {
                yield return item;
            }
            yield break;
        }

        for (int i = 0; i < groupedCommands.Count; i++)
        {
            var gitem = groupedCommands[i];
            foreach (var item in gitem)
            {
                yield return item;
            }
            if (i != groupedCommands.Count - 1)
                yield return new Separator();
        }
    }
}
