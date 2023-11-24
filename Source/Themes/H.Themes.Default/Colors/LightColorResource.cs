using System;
using System.Windows;

namespace H.Themes.Default
{
    public class LightColorResource : IColorResource
    {
        public string Name => "浅色";
        public ResourceDictionary Resource => new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/H.Themes.Default;component/Colors/Light.xaml")
        };
        public override string ToString()
        {
            return Name;
        }
    }
}
