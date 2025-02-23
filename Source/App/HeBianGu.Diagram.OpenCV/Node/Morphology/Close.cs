// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Diagram.OpenCV
{


    [Display(Name = "闭运算", GroupName = "形态学", Description = " 膨胀 + 腐蚀  ，去除大图形内的小图形", Order = 0)]
    public class Close : MorphologyBase
    {
        protected override MorphTypes GetMorphType()
        {
            return MorphTypes.Close;
        }
    }
}
