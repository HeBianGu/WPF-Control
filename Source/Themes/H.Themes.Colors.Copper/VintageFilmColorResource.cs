using H.Themes.Colors;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Copper;

//### **4. 复古胶片风（Vintage Film）**
//** 特点**：暖调褪色感，适合摄影、文艺类应用  
//```xml
//<!-- 背景 -->
//<Color x:Key="Vintage.Background">#F0E6D2</Color> <!-- 奶油纸 -->
//<Color x:Key="Vintage.Surface">#E5D5B8</Color> <!-- 旧书页 -->

//<!-- 主色 -->
//<Color x:Key="Vintage.Primary">#8B5A2B</Color>  <!-- 棕褐 -->
//<Color x:Key="Vintage.Secondary">#6D4C41</Color> <!-- 咖啡 -->
//<Color x:Key="Vintage.Accent">#9C786C</Color>  <!-- 玫瑰棕 -->

//<!-- 文字 -->
//<Color x:Key="Vintage.Text">#4A3A2E</Color>
//<Color x:Key="Vintage.TextLight">#8B7D6E</Color>
[Display(Name = "复古胶片风（推荐）", GroupName = "柔和复古风", Description = "暖调褪色感，适合摄影、文艺类应用", Order = 100, Prompt = "强力推荐")]
public class VintageFilmColorResource : ColorResourceBase
{
    public VintageFilmColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Copper;component/Light.xaml")
    };
}
