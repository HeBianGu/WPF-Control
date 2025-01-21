using H.Themes.Default;
using System;
using System.Windows;

namespace H.Themes.Colors.Gray
{
    public class GrayLightColorResource : IColorResource
    {
        public string Name => "浅灰色";
        public ResourceDictionary Resource => new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/H.Themes.Colors.Gray;component/Light.xaml")
        };

        public override string ToString()
        {
            return this.Name;
        }
    }
}
