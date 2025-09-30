// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Microsoft.Extensions.Options;

namespace H.Controls.TagBox
{
    [Display(Name = "标签管理")]
    public class TagService : TagServiceBase<Tag>
    {
        IOptions<TagOptions> _options;

        public TagService(IOptions<TagOptions> options)
        {
            _options = options;
        }

        public override IList<Tag> Collection => this._options.Value.Tags;

        public override bool Load(out string message)
        {
            message = string.Empty;
            var r = this._options.Value.Load(out message);
            this.OnCollectionChanged();
            return r;
        }

        public override bool Save(out string message)
        {
            return this._options.Value.Save(out message);
        }
    }
}
