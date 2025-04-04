namespace H.Style;

//风格 适用场景    代表框架
//扁平化 通用 Web 应用 Bootstrap, Tailwind
//Material Design 企业级/移动端 MUI, Vuetify
//Neumorphism Dashboard/金融 手动 CSS
//Glassmorphism   创意网站 Tailwind CSS
//极简主义    博客/品牌官网 自定义 CSS
//暗黑模式    夜间模式 Ant Design, MUI
//3D/动画 游戏/数据可视化 Three.js, GSAP
public enum StyleType
{
    /// <summary>
    /// 系统默认样式
    /// </summary>
    None,
    /// <summary>
    /// 扁平化设计：简洁、无阴影、无渐变、纯色块、极简图标
    /// </summary>
    Concise,
    ///// <summary>
    ///// 材质设计：轻微阴影、动画过渡、卡片层级、响应式交互
    ///// </summary>
    //Material,
    ///// <summary>
    ///// 新拟态：柔和的阴影、凸起/凹陷效果、低对比度配色(Dashboard、金融/数据可视化 UI)
    ///// </summary>
    //Neumorphism,
    ///// <summary>
    ///// 暗黑模式：深色背景、高对比度、减少蓝光(夜间模式、开发者工具、视频/图片类网站)
    ///// </summary>
    //DarkMode
}
