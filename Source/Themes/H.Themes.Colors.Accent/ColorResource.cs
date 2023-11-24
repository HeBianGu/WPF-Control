using H.Themes.Default;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace H.Themes.Colors.Accent
{
    public class AccentLightColorResource : IColorResource
    {
        public string Name => "浅主题";
        public ResourceDictionary Resource => new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/H.Themes.Colors.Accent;component/Light.xaml")
        };

        public override string ToString()
        {
            return this.Name;
        }
    }
}
