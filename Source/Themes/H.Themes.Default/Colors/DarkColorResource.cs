using System;
using System.Windows;

namespace H.Themes.Default
{
    public class DarkColorResource : IColorResource
    {
        public string Name => "深色";
        public ResourceDictionary Resource => new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/H.Themes.Default;component/Colors/Dark.xaml")
        };
        public override string ToString()
        {
            return Name;
        }
    }
}
