// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.DB;
using H.Extensions.Common;

namespace H.App.AIDI.Extension;
public static class DbExtensions
{
    public static fm_dd_label CloneData(this fm_dd_label label)
    {
        fm_dd_label result = new fm_dd_label();
        result.LabelColor = label.LabelColor;
        result.LabelDescription = label.LabelDescription;
        result.LabelName = label.LabelName;
        result.BoundingBox = label.BoundingBox;
        return result;
    }

    public static fm_dd_image CloneData(this fm_dd_image src)
    {
        fm_dd_image image = new fm_dd_image();
        image.Name = src.Name;
        image.Url = src.Url;
        image.Extend = src.Extend;
        image.Size = src.Size;
        image.PixelWidth = src.PixelWidth;
        image.PixelHeight = src.PixelHeight;
        image.Tags = src.Tags;
        image.Score = src.Score;
        image.Labels.AddRange(src.Labels.Select(x => x.CloneData()));
        image.Type = src.Type;
        image.Favorite = src.Favorite;
        image.FavoritePath = src.FavoritePath;
        image.PageID = src.PageID;
        image.ProjectID = src.ProjectID;
        return image;
    }

    public static fm_dd_image ToImageEntity(this string file)
    {
        fm_dd_image image = new fm_dd_image();
        image.Name = System.IO.Path.GetFileNameWithoutExtension(file);
        string currentDir = AppDomain.CurrentDomain.BaseDirectory;
        image.Url = file.GetRelativeStartsWithPath(currentDir);
        image.Extend = System.IO.Path.GetExtension(file);
        image.Size = new FileInfo(file).Length;
        var t = file.GetImageResolution();
        image.PixelWidth = t.width;
        image.PixelHeight = t.height;
        return image;
    }
}
