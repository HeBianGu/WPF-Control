// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Interfaces;

namespace H.Presenters.Design.Base;

public interface ICloneableDesignPresenter : IDesignPresenter
{
    ICloneableDesignPresenter Clone();
}

