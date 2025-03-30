using H.Themes.Default.Colors;
using System.Windows;

namespace H.Themes.Colors.Cyberpunk;

//### **1. 深色科技风（Cyberpunk / Dark Tech）**
//** 特点**：霓虹对比色 + 深色背景，适合数据看板、科技感应用  
//```xml
//<!-- 背景 -->
//<Color x:Key="DarkTech.Background">#0D0F14</Color>
//<Color x:Key="DarkTech.Surface">#1A1D26</Color>

//<!-- 霓虹色 -->
//<Color x:Key="DarkTech.Primary">#00F0FF</Color>  <!-- 赛博蓝 -->
//<Color x:Key="DarkTech.Secondary">#FF2A6D</Color> <!-- 荧光粉 -->
//<Color x:Key="DarkTech.Accent">#05E0A7</Color>  <!-- 电子绿 -->

//<!-- 文字 -->
//<Color x:Key="DarkTech.Text">#E0E0E0</Color>
//<Color x:Key="DarkTech.TextDim">#8A8F9D</Color>
public class CyberpunkColorResource : IColorResource
{
    public string Name => "深色科技风";
    public ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Cyberpunk;component/Dark.xaml")
    };

    public override string ToString()
    {
        return this.Name;
    }
}
