// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.TagBox;
using H.Services.Project;
using Microsoft.Extensions.Options;

namespace H.App.AIDI.Base;
public class ProjectTagService : TagService
{
    private readonly IProjectService _project;
    public ProjectTagService(IOptions<TagOptions> options, IProjectService project) : base(options)
    {
        _project = project;
    }

    public override IList<Tag> Collection
    {
        get
        {
            if (_project.Current is AIDIProjectItem item)
                return item.Tags;
            return base.Collection;
        }
    }
}
