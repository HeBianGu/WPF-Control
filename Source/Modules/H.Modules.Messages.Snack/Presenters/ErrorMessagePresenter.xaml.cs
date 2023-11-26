// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Extensions.Geometry;
using System.Windows;

namespace H.Modules.Messages.Snack
{
    public class ErrorMessagePresenter : MessagePresenterBase
    {
        public ErrorMessagePresenter()
        {
            this.Geometry = GeometryFactory.Create(Geometrys.Error);
            this.Level = 4;
        }
    }
}
