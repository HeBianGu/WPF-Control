using H.Themes.Default;
using System;
using System.Windows;

namespace H.Themes.Colors.Blue
{
    public class BlueDarkColorResource : IColorResource
    {
        public string Name => "深蓝色";
        public ResourceDictionary Resource => new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/H.Themes.Colors.Blue;component/Dark.xaml")
        };

        public override string ToString()
        {
            return this.Name;
        }
    }
}
