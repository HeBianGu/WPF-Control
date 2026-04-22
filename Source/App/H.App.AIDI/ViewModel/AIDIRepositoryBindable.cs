// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.DB;
using H.Extensions.DataBase.Repository;
using H.Services.Project;

namespace H.App.AIDI.ViewModel;
public class AIDIRepositoryBindable : RepositoryBindable<fm_dd_image>
{
    protected override bool Where(fm_dd_image entity)
    {
        if (IocProject.Instance.Current is AIDIProjectItem projectItem)
            return projectItem.Where(entity);
        return false;
    }
}



