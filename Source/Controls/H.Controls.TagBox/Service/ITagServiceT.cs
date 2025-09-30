// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.TagBox
{
    public interface ITagService<T> : ITagService where T : ITag
    {
        new IList<T> Collection { get; }

        void Add(params T[] ts);

        bool ContainTag(string name, T tag);

        string ConvertToCheck(string value, T tag);

        string ConvertToUnCheck(string value, T tag);

        new T Create();

        void Delete(params T[] ts);

        new IEnumerable<T> ToTags(string name);
    }
}
