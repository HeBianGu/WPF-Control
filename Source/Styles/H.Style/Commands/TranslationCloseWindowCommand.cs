// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Transitionable;

namespace H.Styles.Commands;

public class TranslationCloseWindowCommand : CloseWindowCommand
{
    public override async void Execute(object parameter)
    {
        if (parameter is Window window)
        {
            var r = await this.ShowDialogMessage(window);
            if (r != true)
                return;

            if (window is ITransitionHostable hostable)
            {
                var task = hostable.Close(window);
                await task.ContinueWith(x =>
                  {
                      SystemCommands.CloseWindow(window);
                  });
            }
        }
    }
}
