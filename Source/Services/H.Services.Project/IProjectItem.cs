// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Project;

public interface IProjectItem : ISaveable, ILoadable
{
    DateTime UpdateTime { get; set; }
    bool IsFixed { get; set; }
    string Title { get; set; }
    string Path { get; set; }
    bool Close(out string message);
    bool Delete(out string message);
}