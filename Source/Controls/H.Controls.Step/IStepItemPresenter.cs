// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Controls.Step
{
    public interface IStepItemPresenter
    {
        string DisplayName { get; set; }
        string Message { get; set; }
        int Percent { get; set; }
        StepState State { get; set; }
        double Width { get; set; }
    }
}