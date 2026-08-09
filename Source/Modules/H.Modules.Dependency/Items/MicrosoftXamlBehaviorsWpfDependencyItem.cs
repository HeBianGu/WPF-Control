// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Dependency.Base;

namespace H.Modules.Dependency.Items;

[Display(Name = "Microsoft.Xaml.Behaviors.Wpf", GroupName = "基础框架", Description = "提供 WPF 行为支持。")]
public class MicrosoftXamlBehaviorsWpfDependencyItem : DependencyItemBase
{
    public MicrosoftXamlBehaviorsWpfDependencyItem()
    {
        Name = "Microsoft.Xaml.Behaviors.Wpf";
        Uri = "https://github.com/microsoft/XamlBehaviorsWpf";
        Version = "1.1.77";
        Licence = "MIT License";
        Description = "提供 WPF 行为支持。";
    }
}
