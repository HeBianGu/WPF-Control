using H.Themes.Default.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Web;

[Display(Name = "Ant Design Pro", GroupName = "网站前端风", Description = "纯色", Order = 100, Prompt = "试用")]
public class AntDesignProColorResource : ColorResourceBase
{
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/AntDesignPro.xaml")
    };
}
