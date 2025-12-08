// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.ValueConverter;

namespace H.Extensions.Mvvm.Commands;

public class GetGroupCommandsValueConverter : MarkupValueConverterBase
{
    public string GroupName { get; set; } = "菜单栏";
    public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is ICommandsBindable bindable)
            return this.CreateCommands(bindable.Commands);
        if (value is IEnumerable<ICommand> commands)
            return this.CreateCommands(commands);
        return DependencyProperty.UnsetValue;
    }

    protected virtual IEnumerable<IDisplayCommand> CreateCommands(IEnumerable<ICommand> commands)
    {
        return commands.OfType<IDisplayCommand>().Where(x => x.GroupName?.Split(',').Contains(this.GroupName) == true).OrderBy(x => x.Order);
    }
}
