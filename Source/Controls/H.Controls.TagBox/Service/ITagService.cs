// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Interfaces;

namespace H.Controls.TagBox
{
    public interface ITagService : IDataSource<ITag>, ISplashLoadable, ISplashSave
    {
        ITag Create();
        string ConvertToCheck(string value, ITag tag);
        string ConvertToUnCheck(string value, ITag tag);
        bool ContainTag(string name, ITag tag);
        IEnumerable<ITag> ToTags(string name);
    }

    public class IocTagService : Ioc<ITagService>
    {

    }
}
