namespace H.Services.Message.IODialog;
public static class IOFileDialogExtension
{
    public static string ShowOpenFile(this IIOFileDialogService dialog, IOpenFilePathable openFilePathable, Action<IIOFileDialogOption> optionAction = null)
    {
        string r = dialog.ShowOpenFile(x =>
        {
            x.Filter = openFilePathable.Filter;
            x.InitialDirectory = openFilePathable.FilePath;
        });
        if (r == null)
            return null;
        openFilePathable.FilePath = r;
        return r;
    }

    public static string ShowOpenFile(this IIOFileDialogService dialog, Action<IIOFileDialogOption> optionAction = null, params string[] filterExtensions)
    {
        string filter = filterExtensions.Select(x => $" *.{x};").Aggregate((x, y) => x + y);
        return dialog.ShowOpenFile(optionAction);
    }
    public static string[] ShowOpenFiles(this IIOFileDialogService dialog, Action<IIOFileDialogOption> optionAction = null, params string[] filterExtensions)
    {
        string filter = filterExtensions.Select(x => $" *.{x};").Aggregate((x, y) => x + y);
        return dialog.ShowOpenFiles(optionAction);
    }

    public static string ShowOpenFile(this IIOFileDialogService dialog, Action<string> sumitAction = null, Action<IIOFileDialogOption> optionAction = null)
    {
        string r = dialog.ShowOpenFile(optionAction);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }
    public static string[] ShowOpenFiles(this IIOFileDialogService dialog, Action<string[]> sumitAction = null, Action<IIOFileDialogOption> optionAction = null)
    {
        string[] r = dialog.ShowOpenFiles(optionAction);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }

    public static string ShowOpenImageFile(this IIOFileDialogService dialog, Action<string> sumitAction = null, Action<IIOFileDialogOption> optionAction = null)
    {
        return dialog.ShowOpenFile(sumitAction, x =>
        {
            x.Filter = IIOFileDialogOption.defaultImageFilter;
            optionAction?.Invoke(x);
        });
    }

    public static string[] ShowOpenImageFiles(this IIOFileDialogService dialog, Action<string[]> sumitAction = null, Action<IIOFileDialogOption> optionAction = null)
    {
        return dialog.ShowOpenFiles(sumitAction, x =>
        {
            x.Filter = IIOFileDialogOption.defaultImageFilter;
            optionAction?.Invoke(x);
        });
    }

    public static string[] ShowOpenVideoFiles(this IIOFileDialogService dialog, Action<string[]> sumitAction = null, Action<IIOFileDialogOption> optionAction = null)
    {
        return dialog.ShowOpenFiles(sumitAction, x =>
        {
            x.Filter = IIOFileDialogOption.defaultVideoFilter;
            optionAction?.Invoke(x);
        });
    }

}

