// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace H.Themes.Default
{
    public static class ThemeTypeExtension
    {
        private const string Colors = nameof(Colors);
        private const string FontSizes = nameof(FontSizes);
        private const string Layouts = nameof(Layouts);
        private const string Format1 = "pack://application:,,,/H.Themes.Default;component/ResourceKeys/{0}.xaml";
        private const string Format2 = "pack://application:,,,/H.Themes.Default;component/ResourceKeys/{0}/{1}.xaml";
        private const string Format3 = "pack://application:,,,/H.Themes.Default;component/ResourceKeys/{0}/{1}/{2}.xaml";

        private static string GetFormat(string arg1) => string.Format(Format1, arg1);
        private static Uri GetUri(string arg1) => new Uri(GetFormat(arg1));
        private static ResourceDictionary GetResource(string arg1) => new ResourceDictionary() { Source = GetUri(arg1) };


        private static string GetFormat(string arg1, string arg2) => string.Format(Format2, arg1, arg2);
        private static Uri GetUri(string arg1, string arg2) => new Uri(GetFormat(arg1, arg2));
        private static ResourceDictionary GetResource(string arg1, string arg2) => new ResourceDictionary() { Source = GetUri(arg1, arg2) };

        private static string GetFormat(string arg1, string arg2, string arg3) => string.Format(Format3, arg1, arg2, arg3);
        private static Uri GetUri(string arg1, string arg2, string arg3) => new Uri(GetFormat(arg1, arg2, arg3));
        private static ResourceDictionary GetResource(string arg1, string arg2, string arg3) => new ResourceDictionary() { Source = GetUri(arg1, arg2, arg3) };

        public static ResourceDictionary GetResource(this ColorThemeType type)
        {
            if (type == ColorThemeType.Default)
                return GetResource(Colors, type.ToString());
            else if (type == ColorThemeType.Dark)
                return GetResource(Colors, type.ToString());
            else if (type == ColorThemeType.DarkGray)
                return GetResource(Colors, "Darks", "Gray");
            else if (type == ColorThemeType.Light)
                return GetResource(Colors, type.ToString());
            else if (type == ColorThemeType.LightAccent)
                return GetResource(Colors, "Lights", "Accent");
            else
                return GetResource(Colors, "Default");
        }

        private static bool IsColorsResource(this ResourceDictionary resource)
        {
            return resource.IsTypeResource(Colors);
        }

        private static bool IsFontSizesResource(this ResourceDictionary resource)
        {
            return resource.IsTypeResource(FontSizes);
        }

        private static bool IsTypeResource(this ResourceDictionary resource, string typeName)
        {
            return resource.Source.AbsoluteUri.StartsWith($"pack://application:,,,/H.Themes.Default;component/ResourceKeys/{typeName}");
        }

        private static bool IsLayoutsResource(this ResourceDictionary resource)
        {
            return resource.IsTypeResource("Layouts");
        }

        public static void ChangeThemeType(this ColorThemeType n)
        {
            var resource = n.GetResource();
            ChangeResourceDictionary(resource, x => x.IsColorsResource());


            //{
            //    var brushResource = GetResource("BrushKeys");
            //    brushResource.ChangeResourceDictionary(x => x.Source.AbsoluteUri == brushResource.Source.AbsoluteUri, true);

            //}

        


            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            //          {
            //              var dic = Application.Current.Resources.MergedDictionaries;
            //              var temp = dic.ToList();
            //              dic.Clear();
            //              foreach (var item in temp)
            //              {
            //                  dic.Add(item);
            //              }
            //          }));


            //{
            //    var brushResource = GetResource("ConciseControls");
            //    brushResource.ChangeResourceDictionary(x => x.Source.AbsoluteUri == brushResource.Source.AbsoluteUri, true);

            //}

        }

        public static void ChangeFontSizeThemeType(this ThemeType n)
        {
            var resource = n.GetFontSizeResource();
            ChangeResourceDictionary(resource, x => x.IsFontSizesResource());
        }


        public static void ChangeLayoutThemeType(this ThemeType n)
        {
            var resource = n.GetLayoutResource();
            ChangeResourceDictionary(resource, x => x.IsLayoutsResource());
        }

        public static void ChangeResourceDictionary(this ResourceDictionary n, Func<ResourceDictionary, bool> predicate, bool force = false)
        {
            var dic = Application.Current.Resources.MergedDictionaries;
            var finds = dic.Where(predicate).ToList();
            if (finds.Count() == 1 && force == false)
            {
                var find = finds.First();
                if (find.Source.AbsoluteUri != n.Source.AbsoluteUri)
                {
                    int index = dic.IndexOf(find);
                    dic[index] = n;
                }
                return;
            }
            foreach (var find in finds)
            {
                dic.Remove(find);
            }
            dic.Add(n);
        }

        public static ResourceDictionary GetLayoutResource(this ThemeType type)
        {
            return GetResource("Layouts", type.ToString());
        }

        public static ResourceDictionary GetFontSizeResource(this ThemeType type)
        {
            return GetResource("FontSizes", type.ToString());
        }
    }
}
