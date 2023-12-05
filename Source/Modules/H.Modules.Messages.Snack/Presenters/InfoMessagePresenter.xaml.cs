// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Geometry;

namespace H.Modules.Messages.Snack
{
    public class InfoMessagePresenter : MessagePresenterBase
    {
        public InfoMessagePresenter()
        {
            this.Geometry = GeometryFactory.Create(Geometrys.Info);
            this.Level = 2;
        }
    }
}
