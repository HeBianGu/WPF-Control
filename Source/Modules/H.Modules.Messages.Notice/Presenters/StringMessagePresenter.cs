// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Geometry;

namespace H.Modules.Messages.Notice
{
    public class StringMessagePresenter : MessagePresenterBase
    {
        public StringMessagePresenter()
        {
            this.Geometry = GeometryFactory.Create(Geometrys.Wait);
        }
    }
}
