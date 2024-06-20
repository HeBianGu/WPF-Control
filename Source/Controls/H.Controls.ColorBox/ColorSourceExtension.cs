
using H.Extensions.Color;

namespace H.Controls.ColorBox
{
    public class ColorSourceExtension : GetDepthColorsExtension
    {
        public ColorSourceExtension()
        {
            this.Depthes = ColorBoxSetting.Instance.Depthes;
            this.From = ColorBoxSetting.Instance.From;
            this.To = ColorBoxSetting.Instance.To;
            this.Step = ColorBoxSetting.Instance.Step;
        }
    }
}
