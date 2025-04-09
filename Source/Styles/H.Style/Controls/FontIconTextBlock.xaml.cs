namespace H.Styles.Controls;

//https://learn.microsoft.com/zh-cn/windows/apps/design/style/segoe-ui-symbol-font?WT.mc_id=MVP_380318
public class FontIconTextBlock : TextBlock
{
    static FontIconTextBlock()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIconTextBlock), new FrameworkPropertyMetadata(typeof(FontIconTextBlock)));
    }
}
