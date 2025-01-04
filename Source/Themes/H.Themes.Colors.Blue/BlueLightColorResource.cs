using H.Themes.Default;
using System;
using System.Windows;

namespace H.Themes.Colors.Blue
{
    public class BlueLightColorResource : IColorResource
    {
        public string Name => "浅蓝色";
        public ResourceDictionary Resource => new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/H.Themes.Colors.Blue;component/Light.xaml")
        };

        public override string ToString()
        {
            return this.Name;
        }
    }
}
