// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.PropertyGrid
{
    public interface IValidateInput
    {
        event InputValidationErrorEventHandler InputValidationError;
        bool CommitInput();
    }
}
