
namespace H.Services.Common;

public class IOFileDialogOption : IIOFileDialogOption
{
    public string Filter { get; set; } = IIOFileDialogOption.defaultFilter;
    public string Title { get; set; } = IIOFileDialogOption.defaultTitle;
    public string InitialDirectory { get; set; }
    public bool RestoreDirectory { get; set; } = true;
}

