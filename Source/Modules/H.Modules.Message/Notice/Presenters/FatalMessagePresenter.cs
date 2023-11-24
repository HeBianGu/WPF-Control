// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Extensions.Geometry;
using System.Windows;

namespace H.Modules.Message
{
    public class FatalMessagePresenter : MessagePresenterBase
    {
        public FatalMessagePresenter()
        {
            this.Geometry = GeometryFactory.Create(Geometrys.Fatal);
            this.Level = 5;
        }
    }
}
