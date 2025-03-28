using H.Services.AppPath;

namespace H.Controls.Diagram.Presenter.DiagramTemplates;

public static class DiagramTemplateExtension
{
    public static string GetDefaultFileName(this IDiagramTempaltes templates)
    {
        return System.IO.Path.Combine(AppPaths.Instance.UserData, "diagramtemplates.json");
    }
}
