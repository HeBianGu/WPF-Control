# Project 项目管理二次开发文档

**适用项目：** `H.Services.Project`、`H.Modules.Project`、业务应用项目  
**核心类型：** `IProjectService`、`IProjectItem`、`ProjectServiceBase<T>`、`ProjectItemBase`、`ProjectOptions`、`IocProject`  
**相关能力：** 项目列表管理、当前项目切换、项目文件保存/加载、项目导入导出、项目启动加载、退出保存、项目选择界面

本文介绍 WPF-Control 中 Project 项目管理模块的详细使用方式，重点说明项目服务注册、项目模型定义、项目文件保存加载、项目切换流程、项目对话框和内置命令，以及如何在业务应用中扩展自己的项目类型。

---

## 1. Project 模块定位

Project 模块用于管理应用中的“项目/工程”数据。它提供：

- 项目列表维护。
- 当前项目 `Current` 管理。
- 项目创建、编辑、查看、删除。
- 项目打开、关闭、保存。
- 项目文件导入/导出。
- 启动时加载项目列表。
- 应用退出或项目切换时保存项目。
- 项目配置加入设置页。
- 项目主视图 Presenter 和缩略图 Presenter。

典型数据结构：

```text
UserProject 文件夹
├── projects.json       // 项目列表元数据
├── 项目1.prj           // 项目1业务数据
├── 项目2.prj           // 项目2业务数据
└── 项目3.prj           // 项目3业务数据
```

---

## 2. 相关项目职责

| 项目 | 说明 |
|---|---|
| `H.Services.Project` | 定义项目服务接口、项目项接口、项目保存服务和 IOC 入口。 |
| `H.Modules.Project` | 提供默认项目服务、项目项基类、项目配置、项目列表界面、项目对话框和命令。 |
| 业务应用项目 | 继承 `ProjectServiceBase<T>` 和 `ProjectItemBase`，实现业务项目数据。 |

---

## 3. 核心接口

### 3.1 `IProjectService`

```csharp
public interface IProjectService : ISplashSave, ISplashLoadable, IDefaultTemplateable
{
    IProjectItem Current { get; set; }
    IProjectItem Create();
    void Add(IProjectItem project);
    Task DeleteAsync(Func<IProjectItem, bool> func);
    IEnumerable<IProjectItem> Where(Func<IProjectItem, bool> func = null);
    Action<IProjectItem, IProjectItem> CurrentChanged { get; set; }
    Action<IProjectItem> ProjectAdded { get; set; }
}
```

职责说明：

| 成员 | 说明 |
|---|---|
| `Current` | 当前打开项目。切换时会保存旧项目、关闭旧项目、加载新项目。 |
| `Create()` | 创建一个新的项目实例。业务项目需要重写。 |
| `Add(project)` | 加入项目列表。 |
| `DeleteAsync(func)` | 按条件删除项目。 |
| `Where(func)` | 查询项目列表。 |
| `CurrentChanged` | 当前项目变化回调。 |
| `ProjectAdded` | 项目新增回调。 |
| `ISplashLoadable.Load` | 启动阶段加载项目列表。 |
| `ISplashSave.Save` | 退出或保存阶段保存项目。 |
| `IDefaultTemplateable.LoadDefaultTemplate` | 从默认项目模板初始化项目。 |

### 3.2 `IProjectItem`

```csharp
public interface IProjectItem : ISaveable, ILoadable
{
    string ID { get; set; }
    DateTime UpdateTime { get; set; }
    bool IsFixed { get; set; }
    bool IsExample { get; set; }
    string Title { get; set; }
    bool Close(out string message);
    Task<(bool success, string message)> DeleteAsync();
    IProjectItemPresenter Presenter { get; }
    IProjectItemThumbnailPresenter ThumbnailPresenter { get; }
}
```

职责说明：

| 成员 | 说明 |
|---|---|
| `ID` | 项目唯一标识。 |
| `Title` | 项目标题，同时默认作为项目文件名。 |
| `UpdateTime` | 修改时间，用于项目列表排序。 |
| `IsFixed` | 是否固定项目。 |
| `IsExample` | 是否示例项目。 |
| `Save` | 保存当前项目业务数据。 |
| `Load` | 加载当前项目业务数据。 |
| `Close` | 关闭项目前释放资源或保存状态。 |
| `DeleteAsync` | 删除项目文件和相关资源。 |
| `Presenter` | 项目主视图展示。 |
| `ThumbnailPresenter` | 项目缩略图展示。 |

### 3.3 `IocProject`

```csharp
public class IocProject : Ioc<IProjectService>
{
}
```

使用示例：

```csharp
IProjectItem current = IocProject.Instance.Current;
await IocProject.Instance.ShowNewProject();
await IocProject.Instance.ShowProjectsDialog();
IocProject.Instance.Save(out string message);
```

---

## 4. 注册项目管理服务

### 4.1 使用默认项目服务

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddSetting();
    services.AddProject();
}
```

`AddProject()` 默认注册：

```csharp
services.TryAdd(ServiceDescriptor.Singleton<IProjectService, ProjectService>());
services.TryAdd(ServiceDescriptor.Singleton<IProjectViewPresenter, ProjectViewPresenter>());
services.TryAdd(ServiceDescriptor.Singleton<IProjectDialogService, ProjectDialogService>());
```

### 4.2 使用自定义项目服务

业务应用通常会使用自定义项目类型：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddProject<MyProjectService>(option =>
    {
        option.Extenstion = ".myprj";
        option.DefaultProjectName = "项目";
        option.SaveMode = ProjectSaveMode.OnProjectChanged;
        option.UseOpenCurrentOnLoad = true;
    });
}
```

参考 `H.App.AIDI`：

```csharp
services.AddProject<AIDIProjectService>(x => x.UseOpenCurrentOnLoad = false);
```

### 4.3 启用项目配置页

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseProjectOptions();
}
```

`UseProjectOptions()` 会执行：

```csharp
IocSetting.Instance.Add(ProjectOptions.Instance);
```

### 4.4 启用项目退出保存

```csharp
services.AddProjectSplashSave();
```

默认实现：

```csharp
public class ProjectSplashSaveService : IProjectSplashSaveService
{
    public string Name => "保存项目";

    public bool Save(out string message)
    {
        message = null;
        return IocProject.Instance.Current?.Save(out message) != false;
    }
}
```

---

## 5. `ProjectOptions` 配置

`ProjectOptions` 是项目模块的设置项：

```csharp
[Display(Name = "工程配置", GroupName = SettingGroupNames.GroupSystem, Description = "工程配置的信息")]
public class ProjectOptions : IocOptionInstance<ProjectOptions>, IProjectOptions
{
}
```

主要配置：

| 属性 | 默认值 | 说明 |
|---|---:|---|
| `Extenstion` | `.prj` | 项目文件扩展名。 |
| `SaveMode` | `OnProjectChanged` | 项目保存时机。 |
| `DefaultProjectName` | `项目` | 新建项目默认名称前缀。 |
| `UseOpenCurrentOnLoad` | `true` | 启动加载项目列表后是否自动打开最近项目。 |

保存时机枚举：

```csharp
public enum ProjectSaveMode
{
    OnAppExit = 0,
    OnProjectChanged
}
```

说明：

- `OnAppExit`：应用退出时保存。
- `OnProjectChanged`：项目列表或当前项目变化时保存。

---

## 6. `ProjectServiceBase<T>` 工作机制

`ProjectServiceBase<T>` 是项目服务基类：

```csharp
public abstract class ProjectServiceBase<T> : CommandsBindableBase, IProjectService, IDataSource<T>
    where T : IProjectItem
{
    public abstract T Create();
}
```

### 6.1 项目列表保存位置

项目列表元数据保存到：

```csharp
private string _projectsPath => Path.Combine(this.GetFolderPath(), "projects.json");
protected virtual string GetFolderPath() => AppPaths.Instance.UserProject;
```

默认目录：

```text
AppPaths.Instance.UserProject
```

### 6.2 项目列表保存

```csharp
public virtual bool Save(out string message)
{
    this.GetSerializer().Save(this._projectsPath, new Projects<T>()
    {
        Items = this.Collection.ToList()
    });
    this.Current?.Save(out message);
    return true;
}
```

保存内容：

- `projects.json`：项目列表信息。
- 当前项目 `.prj` 文件：当前项目业务数据。

### 6.3 项目列表加载

```csharp
public virtual bool Load(out string message)
{
    if (!File.Exists(this._projectsPath))
        return true;

    Projects<T> data = this.GetSerializer().Load<Projects<T>>(this._projectsPath);
    this.Clear();
    if (data != null)
    {
        var orders = data.Items.OrderByDescending(x => x.UpdateTime);
        this.Collection = orders.ToObservable();
    }

    if (ProjectOptions.Instance.UseOpenCurrentOnLoad)
        this.LoadCurrent();

    return true;
}
```

加载后按 `UpdateTime` 倒序排序。

### 6.4 当前项目切换

```csharp
public IProjectItem Current
{
    set
    {
        IProjectItem old = _current;
        _current = value;
        old?.Save(out string message);
        old?.Close(out string messge);
        _current?.Load(out message);
        this.OnCurrentChanged(old, _current);
    }
}
```

切换流程：

1. 保存旧项目。
2. 关闭旧项目。
3. 设置新项目。
4. 加载新项目。
5. 触发 `CurrentChanged`。

---

## 7. 定义自定义项目项

### 7.1 继承 `ProjectItemBase`

```csharp
public class MyProjectItem : ProjectItemBase
{
    [JsonIgnore]
    public override IProjectItemPresenter Presenter => new MyProjectPresenter(this);

    [JsonIgnore]
    public override IProjectItemThumbnailPresenter ThumbnailPresenter => new MyProjectThumbnailPresenter(this);

    [Display(Name = "业务参数")]
    public string BusinessValue { get; set; }

    protected override object GetSaveFileData()
    {
        return new MyProjectData
        {
            BusinessValue = this.BusinessValue
        };
    }

    public override bool Load(out string message)
    {
        message = null;
        if (this.LoadFile<MyProjectData>(out var data) && data != null)
        {
            this.BusinessValue = data.BusinessValue;
        }
        return true;
    }
}
```

### 7.2 项目文件路径

`ProjectItemBase` 默认项目文件路径：

```csharp
public virtual string GetFilePath()
{
    string folder = this.GetFolderPath();
    return Path.Combine(folder, this.Title + ProjectOptions.Instance.Extenstion);
}
```

默认目录：

```csharp
public virtual string GetFolderPath()
{
    return AppPaths.Instance.UserProject;
}
```

如果项目需要独立子目录，可重写：

```csharp
public override string GetFolderPath()
{
    return Path.Combine(AppPaths.Instance.UserProject, this.ID);
}
```

### 7.3 保存业务数据

`ProjectItemBase.Save()` 会调用：

```csharp
object data = this.GetSaveFileData();
this.SaveToFile(data);
this.UpdateTime = DateTime.Now;
IocProject.Instance?.Save(out message);
```

因此业务项目通常重写 `GetSaveFileData()` 返回需要序列化的数据对象。

### 7.4 加载业务数据

使用基类提供的 `LoadFile<T>()`：

```csharp
public override bool Load(out string message)
{
    message = null;
    if (this.LoadFile<MyProjectData>(out var data) && data != null)
    {
        this.BusinessValue = data.BusinessValue;
    }
    return true;
}
```

### 7.5 关闭项目

```csharp
public override bool Close(out string message)
{
    message = null;
    // 释放资源、停止任务、保存临时状态
    return true;
}
```

### 7.6 删除项目

默认删除当前项目文件：

```csharp
public virtual Task<(bool success, string message)> DeleteAsync()
{
    bool r = this.Close(out message);
    if (r == false)
        return Task.FromResult((false, message));

    var filePath = this.GetFilePath();
    if (File.Exists(filePath))
        File.Delete(filePath);

    return Task.FromResult((true, string.Empty));
}
```

如果项目有资源目录、数据库记录、缓存文件，可重写 `DeleteAsync()`。

---

## 8. 定义项目 Presenter

### 8.1 主视图 Presenter

```csharp
public class MyProjectPresenter : ProjectItemPresenter<MyProjectItem>
{
    public MyProjectPresenter(MyProjectItem projectItem) : base(projectItem)
    {
    }
}
```

### 8.2 缩略图 Presenter

```csharp
public class MyProjectThumbnailPresenter : ProjectItemThumbnailPresenter<MyProjectItem>
{
    public MyProjectThumbnailPresenter(MyProjectItem projectItem) : base(projectItem)
    {
    }
}
```

`ProjectItemBase` 中暴露：

```csharp
public override IProjectItemPresenter Presenter => new MyProjectPresenter(this);
public override IProjectItemThumbnailPresenter ThumbnailPresenter => new MyProjectThumbnailPresenter(this);
```

项目列表和主页模块可通过这些 Presenter 展示项目内容和缩略图。

---

## 9. 定义自定义项目服务

```csharp
public class MyProjectService : ProjectServiceBase<MyProjectItem>
{
    public MyProjectService(IOptions<ProjectOptions> options) : base(options)
    {
    }

    public override MyProjectItem Create()
    {
        return new MyProjectItem();
    }
}
```

如果需要使用自定义序列化器，可重写：

```csharp
protected override ISerializerService GetSerializer()
{
    return new NewtonsoftJsonSerializerService();
}
```

参考 `AIDIProjectService`：

```csharp
public class AIDIProjectService : ProjectServiceBase<AIDIProjectItem>
{
    public override AIDIProjectItem Create()
    {
        var result = new AIDIProjectItem();
        result.InputPagePresenter = new InputPagePresenter(result);
        result.SelectedPagePresenter = result.InputPagePresenter;
        result.Tags = this._tagOptions.Value.Tags.ToObservable();
        return result;
    }

    protected override ISerializerService GetSerializer() => new NewtonsoftJsonSerializerService();
}
```

---

## 10. 项目对话框和扩展方法

`ProjectExtension` 为 `IProjectService` 提供常用交互方法。

| 方法 | 说明 |
|---|---|
| `ShowProjectsDialog()` | 显示项目列表选择对话框。 |
| `ShowProjectsOrNewDialog()` | 如果没有当前项目则新建，否则显示项目列表。 |
| `ShowNewProject()` | 显示新建项目表单。 |
| `ShowEidtProject(project)` | 显示编辑项目表单。 |
| `ShowViewProject(project)` | 显示查看项目表单。 |
| `ShowDeleteProject(project)` | 删除项目确认并删除。 |
| `ShowOpenProject(project)` | 打开指定项目。 |
| `ShowSaveProjects()` | 保存项目列表。 |
| `ShowSaveProject(current)` | 保存当前项目。 |
| `ShowCloseProject()` | 保存并关闭当前项目。 |
| `ShowSaveToFile(current)` | 导出项目文件。 |
| `ShowOpenProjectFile()` | 从外部项目文件导入项目。 |
| `ShowCurrentProjectFile(current)` | 使用系统程序打开当前项目文件。 |
| `Contain(projectItem)` | 判断项目是否已在列表中。 |

示例：

```csharp
await IocProject.Instance.ShowNewProject();
await IocProject.Instance.ShowProjectsDialog();
await IocProject.Instance.ShowOpenProject(project);
await IocProject.Instance.ShowSaveProject(IocProject.Instance.Current);
```

---

## 11. 内置项目命令

`H.Modules.Project.Commands` 提供一组可直接在 XAML 或菜单中使用的命令。

| 命令 | 显示名称 | 说明 |
|---|---|---|
| `ShowNewProjectCommand` | 新建项目 | 打开新建项目表单。 |
| `ShowNewProjectWithDefaultNameCommand` | 保存项目列表 | 使用默认名称新建项目。 |
| `ShowProjectsCommand` | 打开项目 | 打开项目列表对话框。 |
| `ShowProjectsOrNewCommand` | 打开项目 | 无当前项目时新建，有当前项目时打开列表。 |
| `ShowOpenProjectCommand` | 打开项目 | 打开指定项目。 |
| `ShowEditProjectCommand` | 编辑项目 | 编辑当前或指定项目。 |
| `ShowViewProjectCommand` | 查看项目 | 查看项目。 |
| `ShowDeleteProjectCommand` | 删除项目 | 删除项目。 |
| `ShowSaveProjectCommand` | 保存项目 | 保存当前项目。 |
| `ShowSaveProjectsCommand` | 保存项目列表 | 保存项目列表。 |
| `ShowSaveProjectToFileCommand` | 导出项目文件 | 导出当前项目文件。 |
| `ShowOpenProjectFileCommand` | 导入项目文件 | 从外部文件导入项目。 |
| `ShowCurrentProjectFileCommand` | 打开项目文件 | 打开当前项目文件。 |
| `ShowCloseProjectCommand` | 关闭项目 | 保存并关闭当前项目。 |

示例：

```xaml
<Button Command="{x:Static projectCommands:ShowNewProjectCommand.Instance}" Content="新建项目" />
```

具体命令暴露方式依赖项目命令基类和 MarkupCommand 使用方式，也可直接调用 `IocProject.Instance` 的扩展方法。

---

## 12. 项目列表界面

### 12.1 `ProjectListViewPresenter`

用于项目列表选择对话框：

```csharp
public class ProjectListViewPresenter : DisplayBindableBase, IProjectListViewPresenter
{
    public IProjectItem SelectedItem { get; set; }
    public bool UseAddProject { get; set; }
    public string SearchText { get; set; }
}
```

`ShowProjectsDialog()` 会创建该 Presenter：

```csharp
ProjectListViewPresenter project = new ProjectListViewPresenter();
project.SelectedItem = projectService.Current;
```

### 12.2 `ProjectDialogService`

`IProjectDialogService` 是项目对话框服务：

```csharp
public class ProjectDialogService : IProjectDialogService
{
    public async Task<bool?> ShowOpenProject(IProjectItem project)
    {
        return await IocProject.Instance.ShowOpenProject(project);
    }

    public async Task<bool?> ShowProjectsDialog()
    {
        return await IocProject.Instance.ShowProjectsDialog(x => x.UseAddProject = true);
    }
}
```

适合在不想直接依赖 `IocProject` 的地方通过服务注入使用。

---

## 13. 项目启动加载和退出保存

`IProjectService` 实现了：

```csharp
ISplashLoadable
ISplashSave
```

因此可加入应用启动和退出流程。

启动加载：

```csharp
IocProject.Instance.Load(out string message);
```

退出保存：

```csharp
IocProject.Instance.Save(out string message);
```

如果注册了 `AddProjectSplashSave()`，则退出保存流程会保存当前项目：

```csharp
services.AddProjectSplashSave();
```

项目服务也可以通过 `Ioc.GetAssignableFromServices<ISplashLoadable>()` 被启动流程扫描加载。

---

## 14. 默认项目模板

`ProjectServiceBase<T>.LoadDefaultTemplate()` 支持从默认项目模板目录初始化项目：

```csharp
public (bool success, string message) LoadDefaultTemplate()
{
    string path = AppDomianPaths.DefaultProjects;
    if (!Directory.Exists(path))
        return (false, "默认项目模版不存在");
    if (File.Exists(_projectsPath))
        return (false, "已存在项目");
    string toPath = this.GetFolderPath();
    path.ToDirectoryEx().BackupToDirectory(toPath);
    return (true, null);
}
```

默认模板目录：

```text
AppDomianPaths.DefaultProjects
```

适用场景：

- 首次启动预置示例项目。
- 安装包内置模板工程。
- 演示或教学模式初始化项目。

---

## 15. 完整最小示例

### 15.1 项目数据

```csharp
public class MyProjectData
{
    public string BusinessValue { get; set; }
}
```

### 15.2 项目项

```csharp
public class MyProjectItem : ProjectItemBase
{
    [JsonIgnore]
    public override IProjectItemPresenter Presenter => new MyProjectPresenter(this);

    [JsonIgnore]
    public override IProjectItemThumbnailPresenter ThumbnailPresenter => new MyProjectThumbnailPresenter(this);

    [Display(Name = "业务参数")]
    public string BusinessValue { get; set; }

    protected override object GetSaveFileData()
    {
        return new MyProjectData
        {
            BusinessValue = this.BusinessValue
        };
    }

    public override bool Load(out string message)
    {
        message = null;
        if (this.LoadFile<MyProjectData>(out var data) && data != null)
            this.BusinessValue = data.BusinessValue;
        return true;
    }
}
```

### 15.3 Presenter

```csharp
public class MyProjectPresenter : ProjectItemPresenter<MyProjectItem>
{
    public MyProjectPresenter(MyProjectItem projectItem) : base(projectItem)
    {
    }
}

public class MyProjectThumbnailPresenter : ProjectItemThumbnailPresenter<MyProjectItem>
{
    public MyProjectThumbnailPresenter(MyProjectItem projectItem) : base(projectItem)
    {
    }
}
```

### 15.4 项目服务

```csharp
public class MyProjectService : ProjectServiceBase<MyProjectItem>
{
    public MyProjectService(IOptions<ProjectOptions> options) : base(options)
    {
    }

    public override MyProjectItem Create()
    {
        return new MyProjectItem();
    }
}
```

### 15.5 注册

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddSetting();
    services.AddProject<MyProjectService>(x =>
    {
        x.Extenstion = ".myprj";
        x.DefaultProjectName = "项目";
        x.UseOpenCurrentOnLoad = true;
        x.SaveMode = ProjectSaveMode.OnProjectChanged;
    });
    services.AddProjectSplashSave();
}

protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseProjectOptions();
}
```

### 15.6 使用

```csharp
await IocProject.Instance.ShowNewProject();
await IocProject.Instance.ShowProjectsDialog();

IProjectItem current = IocProject.Instance.Current;
current.Save(out string message);
```

---

## 16. 二次开发建议

- 业务项目项优先继承 `ProjectItemBase`。
- 业务项目服务优先继承 `ProjectServiceBase<TProjectItem>`。
- 在 `Create()` 中初始化项目默认 Presenter、标签、页面、业务集合等。
- 复杂业务数据建议使用单独 DTO，通过 `GetSaveFileData()` 保存。
- 项目文件名默认来自 `Title`，需要避免非法文件名字符。
- 项目切换会自动保存旧项目并调用 `Close()`，耗时逻辑建议放到等待对话框中处理。
- 如果项目包含大量资源，建议为每个项目创建独立文件夹并重写 `GetFolderPath()`。
- 当前项目变化需要刷新业务 UI 时，使用 `CurrentChanged`。
- 新增项目后需要初始化额外数据时，使用 `ProjectAdded` 或重写 `OnProjectAdded()`。
- 如果不希望启动自动打开最近项目，将 `UseOpenCurrentOnLoad` 设置为 `false`。
- 需要退出自动保存当前项目时注册 `AddProjectSplashSave()`。

---

## 17. 常见问题

### 新建项目后没有保存文件

检查：

1. `ProjectItemBase.GetSaveFileData()` 是否返回了可序列化对象。
2. 是否调用了 `project.Save(out message)` 或 `projectService.Save(out message)`。
3. `ProjectOptions.Extenstion` 是否正确。
4. `AppPaths.Instance.UserProject` 是否有写入权限。

### 启动后没有加载项目列表

检查：

1. 是否注册 `services.AddProject<T>()`。
2. 是否在启动流程调用 `IProjectService.Load(...)`。
3. `projects.json` 是否存在。
4. `UseOpenCurrentOnLoad` 是否为 `true`。

### 项目切换后业务数据没有刷新

检查：

1. `Current` 是否被正确设置。
2. 项目项是否重写 `Load(out message)`。
3. 业务 UI 是否绑定到 `IocProject.Instance.Current` 或监听 `CurrentChanged`。
4. 是否需要在 `Load()` 中刷新仓储、缓存或标签服务。

### 删除项目后文件还存在

默认只删除 `ProjectItemBase.GetFilePath()` 返回的项目文件。如果项目有独立目录、图片、数据库记录等，需要重写 `DeleteAsync()` 手动清理。

### 项目标题重复

`ShowNewProject()` 会使用 `GetIndexSafeName` 生成安全名称，并在提交时检查重复：

```csharp
bool exist = projectService.Where(p => p.Title == x.Title).Count() > 0;
```

如果绕过对话框直接创建项目，需要自行保证标题唯一。

### 项目文件无法导入

检查：

1. `ProjectOptions.Extenstion` 是否和导入文件扩展名一致。
2. `IocMessage.IOFileDialog` 是否可用。
3. 项目项是否继承 `ProjectItemBase`。
4. 目标目录是否有写入权限。
