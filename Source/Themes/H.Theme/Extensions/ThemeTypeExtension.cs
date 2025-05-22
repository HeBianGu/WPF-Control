// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Themes.FontSizes;
using H.Themes.Layouts;

namespace H.Themes.Extensions;

public static class ThemeTypeExtension
{
    private const string Colors = nameof(Colors);
    private const string FontSizes = nameof(FontSizes);
    private const string Layouts = nameof(Layouts);
    private const string Format1 = "pack://application:,,,/H.Theme;component/{0}.xaml";
    private const string Format2 = "pack://application:,,,/H.Theme;component/{0}/{1}.xaml";
    private const string Format3 = "pack://application:,,,/H.Theme;component/{0}/{1}/{2}.xaml";

    private static string GetFormat(string arg1) => string.Format(Format1, arg1);
    private static Uri GetUri(string arg1) => new Uri(GetFormat(arg1));
    private static ResourceDictionary GetResource(string arg1) => new ResourceDictionary() { Source = GetUri(arg1) };

    private static string GetFormat(string arg1, string arg2) => string.Format(Format2, arg1, arg2);
    private static Uri GetUri(string arg1, string arg2) => new Uri(GetFormat(arg1, arg2));
    private static ResourceDictionary GetResource(string arg1, string arg2) => new ResourceDictionary() { Source = GetUri(arg1, arg2) };

    private static string GetFormat(string arg1, string arg2, string arg3) => string.Format(Format3, arg1, arg2, arg3);
    private static Uri GetUri(string arg1, string arg2, string arg3) => new Uri(GetFormat(arg1, arg2, arg3));
    private static ResourceDictionary GetResource(string arg1, string arg2, string arg3) => new ResourceDictionary() { Source = GetUri(arg1, arg2, arg3) };

    private static bool IsFontSizesResource(this ResourceDictionary resource)
    {
        return resource.IsTypeResource(FontSizes);
    }

    private static bool IsTypeResource(this ResourceDictionary resource, string typeName)
    {
        return resource.Source.AbsoluteUri.StartsWith($"pack://application:,,,/H.Theme;component/{typeName}");
    }

    private static bool IsLayoutsResource(this ResourceDictionary resource)
    {
        return resource.IsTypeResource("Layouts");
    }

    public static void ChangeFontSizeThemeType(this FontSizeThemeType n)
    {
        ResourceDictionary resource = n == FontSizeThemeType.Default ? GetResource("FontSizeKeys") : n.GetFontSizeResource();
        resource.ChangeResourceDictionary(x => x.IsFontSizesResource());
    }

    public static void ChangeLayoutThemeType(this LayoutThemeType n)
    {
        ResourceDictionary resource = n == LayoutThemeType.Default ? GetResource("LayoutKeys") : n.GetLayoutResource();
        resource.ChangeResourceDictionary(x => x.IsLayoutsResource());
    }

    public static void ChangeResourceDictionary(this ResourceDictionary n, Func<ResourceDictionary, bool> predicate, bool force = false)
    {
        Collection<ResourceDictionary> dic = Application.Current.Resources.MergedDictionaries;
        List<ResourceDictionary> finds = dic.Where(predicate).ToList();
        if (finds.Count() == 1 && force == false)
        {
            ResourceDictionary find = finds.First();
            if (find.Source.AbsoluteUri != n.Source.AbsoluteUri)
            {
                int index = dic.IndexOf(find);
                dic[index] = n;
            }
            return;
        }
        foreach (ResourceDictionary find in finds)
        {
            dic.Remove(find);
        }
        dic.Add(n);
    }

    public static void RefreshResourceDictionary(this ResourceDictionary n)
    {
        n.ChangeResourceDictionary(x => x.Source.AbsoluteUri == n.Source.AbsoluteUri, true);
    }

    public static ResourceDictionary GetLayoutResource(this LayoutThemeType type)
    {
        if (type == LayoutThemeType.Default)
            return GetResource("LayoutKeys");
        return GetResource("Layouts", type.ToString());
    }

    public static ResourceDictionary GetFontSizeResource(this FontSizeThemeType type)
    {
        if (type == FontSizeThemeType.Default)
            return GetResource("FontSizeKeys");
        return GetResource("FontSizes", type.ToString());
    }

    public static ResourceDictionary GetSystemsResource()
    {
        return GetResource("SystemKeys");
    }

    public static void RefreshBrushResourceDictionary()
    {
        ResourceDictionary brushResource = new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Theme;component/BrushKeys.xaml", UriKind.Absolute) };
        //brushResource.ChangeResourceDictionary(x => x.Source.AbsoluteUri == brushResource.Source.AbsoluteUri, true);
        brushResource.RefreshResourceDictionary();
    }

}
