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

    public static class TagExtension
    {
        public static ITag GetTagByName(this ITagService service, string tagName)
        {
            IEnumerable<ITag> tags = service.Collection.Where(x => x.GroupName == null);
            return tags.FirstOrDefault(x => x.Name == tagName);
        }


        public static string ToUnCheckTagString(this string src, ITag tag)
        {
            return IocTagService.Instance.ConvertToUnCheck(src, tag);
        }

        public static string ToUnCheckTagString(this string src, string tagName)
        {
            var tag = IocTagService.Instance.GetTagByName(tagName);
            return src.ToUnCheckTagString(tag);
        }


        public static string ToCheckTagString(this string src, ITag tag)
        {
            return IocTagService.Instance.ConvertToCheck(src, tag);
        }

        public static string ToCheckTagString(this string src, string tagName)
        {
            bool contain = src.ContainTag(tagName);
            if (!contain)
                return src.ToCheckTagString(tagName);
            return src;
        }

        public static string TryToCheckTagString(this string src, string tagName)
        {
            var tag = IocTagService.Instance.GetTagByName(tagName);
            return src.ToCheckTagString(tag);
        }

        public static bool ContainTag(this string src, ITag tag)
        {
            return IocTagService.Instance.ContainTag(src, tag);
        }
        public static bool ContainTag(this string src, string tagName)
        {
            var tag = IocTagService.Instance.GetTagByName(tagName);
            return src.ContainTag(tag);
        }
    }
}
