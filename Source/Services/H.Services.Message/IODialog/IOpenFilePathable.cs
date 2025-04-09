namespace H.Services.Message.IODialog;

public interface IOpenFilePathable
{
    string FilePath { get; set; }
    string Filter { get; set; }
}

