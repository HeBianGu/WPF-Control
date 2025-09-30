// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Reflection;

namespace H.Extensions.ValueConverter;

public static partial class ConverterEx
{
    #region - Image -
    public static IValueConverter GetImagePixelDisplay => new ConverterBase<string, string>(x =>
    {
        if (x == null)
            return null;
        string filePath = x.ToString();
        var tuple = filePath.ToImageEx().GetImagePixel();
        if (tuple == null)
            return null;
        return tuple.Item1 + "×" + tuple.Item2;
    });

    public static IValueConverter GetFileImageSourceInMemory => new ConverterBase<string, int, ImageSource>((x, p) =>
    {
        if (x == null)
            return null;
        string filePath = x.ToString();
        return filePath.ToImageEx().GetImageSource(p, 0, true);
    });

    #endregion

    #region - Directory -
    public static IValueConverter GetAllFile => new ConverterBase<string, List<string>>(x => x.ToDirectoryEx().GetAllFiles());

    #endregion



    public static IValueConverter GetCommands => new ConverterBase<object, IList<ICommand>>(x =>
    {
        var ps = x.GetType().GetProperties().Where(k => typeof(ICommand).IsAssignableFrom(k.PropertyType));
        List<ICommand> result = new List<ICommand>();
        foreach (var item in ps)
        {
            if (item.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
                continue;
            DisplayAttribute da = item.GetCustomAttribute<DisplayAttribute>(true);
            if (item.GetValue(x) is ICommand command)
            {
                if (command is IDisplayCommand display)
                {
                    display.Name = display.Name ?? da.Name;
                    display.Description = display.Description ?? da.Description;
                    display.Order = da.Order;
                    display.GroupName = display.GroupName ?? da.GroupName;
                }
                result.Add(command);
            }
        }
        return result.OrderBy(k => k is IDisplayCommand d ? d.Order : 0).ToList();
    });
}
