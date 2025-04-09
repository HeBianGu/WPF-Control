namespace H.Services.Message.IODialog;
public interface IIOFileDialogService
{
    string ShowOpenFile(Action<IIOFileDialogOption> optionAction);
    string[] ShowOpenFiles(Action<IIOFileDialogOption> optionAction);
    string ShowSaveFile(Action<IIOSaveFileDialogOption> optionAction);
}

