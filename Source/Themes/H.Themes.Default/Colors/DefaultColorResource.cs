using System;
using System.Windows;

namespace H.Themes.Default
{
    public class DefaultColorResource : IColorResource
    {
        public string Name => "默认";
        public ResourceDictionary Resource => new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/H.Themes.Default;component/Colors/Default.xaml")
        };
        public override string ToString()
        {
            return Name;
        }
    }
}
