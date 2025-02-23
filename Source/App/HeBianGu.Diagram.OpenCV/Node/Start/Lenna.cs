// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase




namespace HeBianGu.Diagram.OpenCV
{



    [Display(Name = "头像", GroupName = "数据源", Order = 0)]
    public class Lenna : StartNodeDataBase
    {
        protected override string GetImagePath()
        {
            return ImagePath.Lenna;
        }
    }
}
