
namespace H.Modules.Project;

public interface IProjectOptions
{
    string DefaultProjectFolder { get; set; }
    string DefaultProjectName { get; set; }
    string Extenstion { get; set; }
    ProjectSaveMode SaveMode { get; set; }

    IJsonSerializerService JsonSerializerService { get; set; }
}