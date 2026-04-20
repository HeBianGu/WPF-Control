// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Threading.Tasks;

namespace H.Modules.Project;

public class SelectProjectComboBox : ComboBox
{
    static SelectProjectComboBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectProjectComboBox), new FrameworkPropertyMetadata(typeof(SelectProjectComboBox)));
    }
    protected override async void OnSelectionChanged(SelectionChangedEventArgs e)
    {
        base.OnSelectionChanged(e);
        if (!this.IsMouseCaptureWithin)
            return;
        await IocProject.Instance.ShowOpenProject(this.SelectedItem as IProjectItem);
    }
}
