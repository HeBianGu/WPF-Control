// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase




using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeBianGu.Diagram.OpenCV
{

    
    
    [Display(Name = "山魈", GroupName = "数据源", Order = 0)]
    public class Mandrill : StartNodeDataBase
    {
        protected override string GetImagePath()
        {
            return ImagePath.Mandrill;
        }
    }
}
