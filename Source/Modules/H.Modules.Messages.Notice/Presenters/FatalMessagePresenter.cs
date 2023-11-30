// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Geometry;
using System.Windows;

namespace H.Modules.Messages.Notice
{
    public class FatalMessagePresenter : MessagePresenterBase
    {
        public FatalMessagePresenter()
        {
            Geometry = GeometryFactory.Create(Geometrys.Fatal);
            Level = 5;
        }
    }
}
