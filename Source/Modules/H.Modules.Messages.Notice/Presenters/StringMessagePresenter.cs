// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Extensions.Geometry;
using System.Windows;

namespace H.Modules.Messages.Notice
{
    public class StringMessagePresenter : MessagePresenterBase
    {
        public StringMessagePresenter()
        {
            Geometry = GeometryFactory.Create(Geometrys.Wait);
        }
    }
}
