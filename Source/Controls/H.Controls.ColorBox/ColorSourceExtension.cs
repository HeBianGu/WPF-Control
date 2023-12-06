
using H.Extensions.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media;

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
