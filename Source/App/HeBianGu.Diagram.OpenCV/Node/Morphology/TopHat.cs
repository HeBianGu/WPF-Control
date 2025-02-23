// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using OpenCvSharp;

namespace HeBianGu.Diagram.OpenCV
{
    [Display(Name = "顶帽", GroupName = "形态学", Description = " 原图 - 开运算  ，大图形外的小图形", Order = 0)]
    public class TopHat : MorphologyBase
    {
        protected override MorphTypes GetMorphType()
        {
            return MorphTypes.TopHat;
        }
    }
}
