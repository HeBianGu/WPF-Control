namespace H.Services.Message.IODialog;

public class ShowOpenFilePathableDialog : DisplayMarkupCommandBase
{
    public override Task ExecuteAsync(object parameter)
    {
        if (parameter is IOpenFilePathable file)
            IocMessage.IOFileDialog.ShowOpenFile(file);
        return base.ExecuteAsync(parameter);
    }
}

