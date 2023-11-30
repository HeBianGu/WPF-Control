// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Geometry;
using System.Windows;

namespace H.Modules.Messages.Snack
{
    public class SuccessMessagePresenter : MessagePresenterBase
    {
        public SuccessMessagePresenter()
        {
            Geometry = GeometryFactory.Create(Geometrys.Success);
            Level = 1;
        }
    }
}
