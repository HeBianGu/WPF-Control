using H.Themes.Default.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Technology;

//### **3. 极简未来（Minimal Futurism）**
//- ** 配色方案**：  
//  `#FFFFFF` (纯白) | `#F2F2F2` (浅灰) | `#2D2D2D` (炭黑) | `#00C4FF` (科技蓝) | `#FF3E41` (警示红)  
//- ** 特点**：干净简洁，突出功能感，适合App、智能设备界面。
[Display(Name = "极简未来（Minimal Futurism）", GroupName = "深色科技风", Description = "干净简洁，突出功能感，适合App、智能设备界面", Order = 100, Prompt = "试用")]
public class MinimalFuturismColorResource : ColorResourceBase
{
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/MinimalFuturism.xaml")
    };
}
