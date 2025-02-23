// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase




namespace HeBianGu.Diagram.OpenCV
{



    [Display(Name = "合照", GroupName = "数据源", Order = 0)]
    public class Yalta : StartNodeDataBase
    {
        protected override string GetImagePath()
        {
            return ImagePath.Yalta;
        }
    }
}
