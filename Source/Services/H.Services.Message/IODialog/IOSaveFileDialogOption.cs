namespace H.Services.Message.IODialog;

public class IOSaveFileDialogOption : IOFileDialogOption, IIOSaveFileDialogOption
{
    public string DefaultExt { get; set; } = "txt";

    public string DefaultFileName { get; set; } = "默认文件名";
}

