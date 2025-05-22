// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Common.MainWindow;

namespace H.Windows.Main;

public class MainWindowSavableService : IMainWindowSavableService
{
    public string Name => "主窗口配置";

    public void Load(Window window)
    {
        window.WindowStartupLocation = MainWindowOption.Instance.WindowStartupLocation;
        window.WindowState = MainWindowOption.Instance.WindowState;
        window.Width = MainWindowOption.Instance.Width;
        window.Height = MainWindowOption.Instance.Height;
    }

    public bool Save(out string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            if (Application.Current.MainWindow is Window window)
            {
                MainWindowOption.Instance.WindowStartupLocation = window.WindowStartupLocation;
                MainWindowOption.Instance.WindowState = window.WindowState;
                MainWindowOption.Instance.Width = window.Width;
                MainWindowOption.Instance.Height = window.Height;
            }
        });
        return MainWindowOption.Instance.Save(out message);
    }
}
