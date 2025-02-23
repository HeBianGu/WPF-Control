// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using OpenCvSharp;

namespace HeBianGu.Diagram.OpenCV
{
    
    
    [Display(Name = "黑帽", GroupName = "形态学", Description = " 原图 - 闭运算  ，大图形内的小图形", Order = 0)]
    public class BlackHat : MorphologyBase
    {
        protected override MorphTypes GetMorphType()
        {
            return MorphTypes.BlackHat;
        }
    }
}
