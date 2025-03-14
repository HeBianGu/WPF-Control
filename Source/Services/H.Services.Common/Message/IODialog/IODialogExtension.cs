namespace H.Services.Common;

public static class IODialogExtension
{
    public static string ShowOpenFile(this IIOFileDialogService dialog, params string[] filterExtensions)
    {
        var filter = filterExtensions.Select(x => $" *.{x};").Aggregate((x, y) => x + y);
        return dialog.ShowOpenFile(filter);
    }

    public static string[] ShowOpenFiles(this IIOFileDialogService dialog, params string[] filterExtensions)
    {
        var filter = filterExtensions.Select(x => $" *.{x};").Aggregate((x, y) => x + y);
        return dialog.ShowOpenFiles(filter);
    }

    public static string ShowOpenImageFile(this IIOFileDialogService dialog, Action<string> sumitAction)
    {
        var r = dialog.ShowOpenFile(IIOFileDialogService.defaultImageFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }

    public static string[] ShowOpenImageFiles(this IIOFileDialogService dialog, Action<string[]> sumitAction)
    {
        var r = dialog.ShowOpenFiles(IIOFileDialogService.defaultImageFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }
    public static string ShowOpenVedioFile(this IIOFileDialogService dialog, Action<string> sumitAction)
    {
        var r = dialog.ShowOpenFile(IIOFileDialogService.defaultVideoFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }
    public static string[] ShowOpenVedioFiles(this IIOFileDialogService dialog, Action<string[]> sumitAction)
    {
        var r = dialog.ShowOpenFiles(IIOFileDialogService.defaultVideoFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }
    public static string ShowOpenAudioFile(this IIOFileDialogService dialog, Action<string> sumitAction)
    {
        var r = dialog.ShowOpenFile(IIOFileDialogService.defaultAudioFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }
    public static string[] ShowOpenAudioFiles(this IIOFileDialogService dialog, Action<string[]> sumitAction)
    {
        var r = dialog.ShowOpenFiles(IIOFileDialogService.defaultAudioFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }

    public static string ShowOpenExcelFile(this IIOFileDialogService dialog, Action<string> sumitAction)
    {
        var r = dialog.ShowOpenFile(IIOFileDialogService.defaultExcelFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }
    public static string[] ShowOpenExcelFiles(this IIOFileDialogService dialog, Action<string[]> sumitAction)
    {
        var r = dialog.ShowOpenFiles(IIOFileDialogService.defaultExcelFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }

    public static string ShowOpenPDFFile(this IIOFileDialogService dialog, Action<string> sumitAction)
    {
        var r = dialog.ShowOpenFile(IIOFileDialogService.defaultPdfFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }
    public static string[] ShowOpenPDFFiles(this IIOFileDialogService dialog, Action<string[]> sumitAction)
    {
        var r = dialog.ShowOpenFiles(IIOFileDialogService.defaultPdfFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }

    public static string ShowOpenZipFile(this IIOFileDialogService dialog, Action<string> sumitAction)
    {
        var r = dialog.ShowOpenFile(IIOFileDialogService.defaultZipFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }
    public static string[] ShowOpenZipFiles(this IIOFileDialogService dialog, Action<string[]> sumitAction)
    {
        var r = dialog.ShowOpenFiles(IIOFileDialogService.defaultZipFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }

    public static string ShowOpenJsonFile(this IIOFileDialogService dialog, Action<string> sumitAction)
    {
        var r = dialog.ShowOpenFile(IIOFileDialogService.defaultJsonFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }
    public static string[] ShowOpenJsonFiles(this IIOFileDialogService dialog, Action<string[]> sumitAction)
    {
        var r = dialog.ShowOpenFiles(IIOFileDialogService.defaultJsonFilter);
        if (r == null)
            return null;
        sumitAction?.Invoke(r);
        return r;
    }
}

