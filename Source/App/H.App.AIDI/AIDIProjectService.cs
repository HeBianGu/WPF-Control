// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.TagBox;
using H.Extensions.Common;
using H.Extensions.NewtonsoftJson;
using H.Modules.Project;
using H.Services.Serializable;
using Microsoft.Extensions.Options;

namespace H.App.AIDI;
public class AIDIProjectService : ProjectServiceBase<AIDIProjectItem>
{
    private readonly IOptions<ProjectOptions> _options;
    private readonly IOptions<TagOptions> _tagOptions;
    public AIDIProjectService(IOptions<ProjectOptions> options, IOptions<TagOptions> tagOptions) : base(options)
    {
        _options = options;
        _tagOptions = tagOptions;
    }

    public override AIDIProjectItem Create()
    {
        var result = new AIDIProjectItem();
        result.InputPagePresenter = new Presenters.InputPagePresenter(result);
        result.SelectedPagePresenter = result.InputPagePresenter;
        result.Tags = this._tagOptions.Value.Tags.ToObservable();
        return result;
    }

    protected override ISerializerService GetSerializer() => new NewtonsoftJsonSerializerService();
}
