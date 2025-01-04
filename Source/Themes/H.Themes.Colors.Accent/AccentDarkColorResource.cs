using H.Themes.Default;
using System;
using System.Windows;

namespace H.Themes.Colors.Accent
{
    public class AccentDarkColorResource : IColorResource
    {
        public string Name => "深主题";
        public ResourceDictionary Resource => new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/H.Themes.Colors.Accent;component/Dark.xaml")
        };

        public override string ToString()
        {
            return this.Name;
        }
    }
}
