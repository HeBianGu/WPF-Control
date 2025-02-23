// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using OpenCvSharp;

namespace HeBianGu.Diagram.OpenCV
{
    
    
    [Display(Name = "梯度", GroupName = "形态学", Description = " 原图 - 腐蚀  ，求图形的边缘", Order = 0)]
    public class Gradient : MorphologyBase
    {
        protected override MorphTypes GetMorphType()
        {
            return MorphTypes.Gradient;
        }
    }
}
