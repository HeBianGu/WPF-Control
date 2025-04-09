using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Mineral;

//### **2. 自然矿物风（Mineral / Earth Tone）**
//** 特点**：低饱和度大地色系，柔和自然，适合环保、健康类应用  
//```xml
//<!-- 背景 -->
//<Color x:Key="Mineral.Background">#F5F3EF</Color> <!-- 沙白 -->
//<Color x:Key="Mineral.Surface">#E8E1D9</Color> <!-- 砂岩 -->

//<!-- 主色 -->
//<Color x:Key="Mineral.Primary">#6B8E8C</Color>  <!-- 灰绿 -->
//<Color x:Key="Mineral.Secondary">#B38D6A</Color> <!-- 陶土 -->
//<Color x:Key="Mineral.Accent">#D4B483</Color>  <!-- 琥珀 -->

//<!-- 文字 -->
//<Color x:Key="Mineral.Text">#3E3A36</Color>
//<Color x:Key="Mineral.TextLight">#7A756E</Color>

/// <summary>
/// 自然矿物风（Mineral / Earth Tone）低饱和度大地色系，柔和自然，适合环保、健康类应用  
/// </summary>
[Display(Name = "自然矿物风（长期支持）", GroupName = "柔和复古风", Description = "低饱和度大地色系，柔和自然，适合环保、健康类应用", Order = 10, Prompt = "长期支持")]
public class MineralColorResource : ColorResourceBase
{
    public MineralColorResource()
    {
        this.IsDark = false;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Mineral;component/Light.xaml")
    };
}
