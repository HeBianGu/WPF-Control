// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Extensions.Geometry;
using System.Windows;

namespace H.Modules.Messages.Snack
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
