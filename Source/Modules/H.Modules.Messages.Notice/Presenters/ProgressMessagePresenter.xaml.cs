// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Geometry;
using H.Providers.Ioc;
using System;
using System.Windows;

namespace H.Modules.Messages.Notice
{
    public class ProgressMessagePresenter : MessagePresenterBase, IPercentNoticeItem
    {
        public ProgressMessagePresenter()
        {
            this.Geometry = GeometryFactory.Create(Geometrys.Wait);
        }
        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }
    }
}
