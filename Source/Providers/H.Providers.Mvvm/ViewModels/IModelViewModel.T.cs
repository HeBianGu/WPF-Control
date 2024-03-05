// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Providers.Mvvm
{
    public interface IModelViewModel<T> : IModelViewModel
    {
        T Model { get; set; }
        double Value { get; set; }
        bool Visible { get; set; }
    }
}
