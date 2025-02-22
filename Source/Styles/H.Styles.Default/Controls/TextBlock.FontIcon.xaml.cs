using H.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace H.Styles.Default
{
    //https://learn.microsoft.com/zh-cn/windows/apps/design/style/segoe-ui-symbol-font?WT.mc_id=MVP_380318
    public class FontIcon : TextBlock
    {
        static FontIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIcon), new FrameworkPropertyMetadata(typeof(FontIcon)));
        }
    }

    public class GetIconSegoesExtension : MarkupExtension
    {
        public int From { get; set; } = 0xE700;
        public int To { get; set; } = 0xE900;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var ds = IconSegoeProvider.GetIconSegoes(this.From, this.To);
            return new IconSegoes(ds);
        }
    }

    public static class IconSegoeProvider
    {
        public static IEnumerable<IconSegoe> GetIconSegoes()
        {
            return GetIconSegoes(0xE000, 0xEFFF);
        }

        public static IEnumerable<IconSegoe> GetIconSegoes(int from, int to)
        {
            for (int codePoint = from; codePoint <= to; codePoint++)
            {
                string unicodeChar = char.ConvertFromUtf32(codePoint);
                yield return new IconSegoe() { CodePoint = codePoint, Key = codePoint.ToKey(), Value = unicodeChar, CodeKey = codePoint.ToCodeKey() };
            }
        }

        private static string ToKey(this int v)
        {
            return $"&#x{v:X};";
        }

        public static string ToCodeKey(this int v)
        {
            return $"\\x{v:X}";
        }
        public static string ToIconSegoe(this int v)
        {
            return v.ToString().ToIconSegoe();
        }
        public static string ToIconSegoe(this string v)
        {
            string unicodeString = "E" + v;
            int unicodeValue = Convert.ToInt32(unicodeString, 16);
            string unicodeChar = char.ConvertFromUtf32(unicodeValue);
            return unicodeChar;
        }
    }

    public class IconSegoes : ObservableCollection<IconSegoe>
    {
        public IconSegoes(IEnumerable<IconSegoe> collection) : base(collection)
        {
        }
    }
    public class IconSegoe : BindableBase
    {
        public int CodePoint { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string CodeKey { get; set; }
        public RelayCommand CopyCommand => new RelayCommand((s, e) =>
        {
            Clipboard.SetText(this.Key);
        });

        public RelayCommand CopyCodeKeyCommand => new RelayCommand((s, e) =>
        {
            Clipboard.SetText(this.CodeKey);
        });
    }
}
