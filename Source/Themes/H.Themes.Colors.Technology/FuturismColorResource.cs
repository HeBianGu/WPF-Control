using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Technology;
//### **5. 未来荧光风（Neon Futurism）**
//** 特点**：高饱和荧光色 + 深空背景，适合游戏、潮流应用  
//```xml
//<!-- 背景 -->
//<Color x:Key="Neon.Background">#121212</Color>
//<Color x:Key="Neon.Surface">#1E1E1E</Color>

//<!-- 荧光色 -->
//<Color x:Key="Neon.Primary">#FF00F5</Color>  <!-- 品红 -->
//<Color x:Key="Neon.Secondary">#00FFA3</Color> <!-- 荧光绿 -->
//<Color x:Key="Neon.Accent">#FFEE00</Color>  <!-- 镉黄 -->

//<!-- 文字 -->
//<Color x:Key="Neon.Text">#FFFFFF</Color>
//<Color x:Key="Neon.TextGlow">#A0A0A0</Color> <!-- 发光弱化 -->
[Display(Name = "未来荧光风", GroupName = "深色科技风", Description = "高饱和荧光色 + 深空背景，适合游戏、潮流应用 ", Order = 100, Prompt = "试验")]
public class FuturismColorResource : ColorResourceBase
{
    public FuturismColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/Futurism.xaml")
    };
}
