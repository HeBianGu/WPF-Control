**适用项目：** `H.Modules.Upgrade`  
**核心类型：** `IUpgradeService`、`UpdateService`、`UpgradeOptions`、`UpdateXmlInfo`、`ShowUpgradeCommand`  
**相关能力：** 在线版本检查、更新说明展示、安装包下载、浏览器下载、安装包启动

本文介绍 `H.Modules.Upgrade` 的版本清单格式、宿主注册、手动检查、启动检查、下载安装和二次开发方式。

---

## 1. 模块定位

`H.Modules.Upgrade` 提供轻量级桌面应用更新流程：

1. 从配置的 HTTP/HTTPS 地址读取 XML 更新清单。
2. 将清单版本与入口程序集版本比较。
3. 检测到新版本时显示更新说明。
4. 由用户选择立即升级。
5. 使用内置下载器保存安装包，或交给系统浏览器下载。
6. 下载完成后通过系统 Shell 启动 `.msi`、`.exe` 或 `.zip` 文件。

典型流程：

```text
ShowUpgradeCommand / IUpgradeService.Upgrade()
    ↓
读取 UpgradeOptions.Uri
    ↓
下载并反序列化 XML 清单
    ↓
比较清单版本与入口程序集版本
    ↓
显示 UpgradePresenter
    ↓
浏览器下载或下载到 SavePath
    ↓
询问是否立即安装
    ↓
Process.Start(安装包)
```

该模块不是完整的增量更新或自更新引擎，不负责进程替换、文件回滚、签名验证、哈希校验和版本依赖处理。

---

## 2. 项目引用

项目引用：

```xml
<ItemGroup>
  <ProjectReference Include="..\..\Modules\H.Modules.Upgrade\H.Modules.Upgrade.csproj" />
</ItemGroup>
```

模块需要框架的设置、消息、对话框和 Web XML 序列化能力。使用默认更新界面前，应注册对应的消息服务。

---

## 3. 基础注册

在基于 `ApplicationBase` 的应用中注册：

```csharp
using H.Extensions.ApplicationBase;
using H.Modules.Upgrade;
using H.Services.AppPath;
using Microsoft.Extensions.DependencyInjection;

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddSetting();
        services.AddWindowMessage();
        services.AddWindowDialogMessage();

        services.AddAutoUpgrade(options =>
        {
            options.Uri = "https://updates.example.com/MyApp/update.xml";
            options.SavePath = AppPaths.Instance.Cache;
            options.LoadFormat = "正在下载 {0}/{1}";
            options.UseIEDownload = false;
        });
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);

        app.UseUpgrade(options =>
        {
            options.Uri = "https://updates.example.com/MyApp/update.xml";
            options.SavePath = AppPaths.Instance.Cache;
            options.LoadFormat = "正在下载 {0}/{1}";
            options.UseIEDownload = false;
        });
    }
}
```

扩展方法职责：

| 方法 | 作用 |
|---|---|
| `AddAutoUpgrade()` | 注册 `IUpgradeService` 和默认 `IWebXmlSerializerService`。 |
| `UseUpgrade()` | 配置 `UpgradeOptions.Instance`，并将其加入设置系统。 |

`UpgradeOptions` 的默认设置文件保存在：

```text
AppPaths.Instance.Config/UpgradeOptions.json
```

应用路径服务说明见 [`development-guide-apppaths.md`](development-guide-apppaths.md)，设置持久化说明见 [`development-guide-setting.md`](development-guide-setting.md)。

---

## 4. 更新清单格式

默认服务要求远程地址返回以下 XML：

```xml
<?xml version="1.0" encoding="utf-8"?>
<item>
  <url>https://updates.example.com/MyApp/MyApp-1.3.1.exe</url>
  <changelog>修复启动异常;优化数据加载速度;增加导出功能</changelog>
  <version>1.3.1.0</version>
  <force>false</force>
</item>
```

字段说明：

| XML 元素 | `UpdateXmlInfo` 属性 | 说明 |
|---|---|---|
| `url` | `Url` | 安装包下载地址。 |
| `changelog` | `Changelog` | 更新说明，当前实现按英文分号 `;` 拆分。 |
| `version` | `Version` | 新版本号，必须能由 `System.Version` 解析。 |
| `force` | `Force` | 强制更新标记。当前默认流程尚未正确传递该值。 |

版本建议使用四段数字：

```text
主版本.次版本.修订号.内部版本
```

例如：

```text
1.3.1.0
```

当前比较逻辑：

```csharp
new Version(update.Version) >
Assembly.GetEntryAssembly().GetName().Version
```

因此：

- 不支持 `1.3.1-beta` 等语义化预发布版本。
- 清单版本格式错误会导致 `Version` 构造异常。
- 比较对象是入口程序集版本，不是文件产品版本或 NuGet 包版本。
- 应确保项目的 `Version`、`AssemblyVersion` 与发布清单策略一致。

项目版本示例：

```xml
<PropertyGroup>
  <Version>1.3.0</Version>
  <AssemblyVersion>1.3.0.0</AssemblyVersion>
  <FileVersion>1.3.0.0</FileVersion>
</PropertyGroup>
```

---

## 5. `UpgradeOptions`

| 属性 | 初始字段值 | `DefaultValue` | 说明 |
|---|---:|---:|---|
| `Uri` | `null` | 无 | XML 更新清单地址。 |
| `SavePath` | `AppPaths.Instance.Cache` | 无 | 下载文件保存目录。 |
| `LoadFormat` | `null` | `正在下载 {0}/{1}` | 下载进度文本格式。 |
| `UseIEDownload` | `false` | `false` | 是否交给系统浏览器下载。 |
| `CheckUpdateOnStart` | `false` | `true` | 启动检查开关。仅在服务作为启动加载项执行时生效。 |
| `AutomaticUpgrade` | `false` | `true` | 自动安装设置。当前默认服务尚未使用。 |
| `NotifyUpgrade` | `false` | `false` | 仅提醒设置。当前默认服务尚未使用。 |

`DefaultValueAttribute` 表示执行“恢复默认”时采用的值，不等于对象字段初始化值。为避免首次运行行为依赖设置加载流程，建议在 `UseUpgrade()` 中显式配置关键属性。

最低必需配置：

```csharp
app.UseUpgrade(options =>
{
    options.Uri = "https://updates.example.com/MyApp/update.xml";
    options.LoadFormat = "正在下载 {0}/{1}";
});
```

`LoadFormat` 必须保留两个格式参数：

- `{0}`：已下载大小。
- `{1}`：总大小。

缺少或使用无效格式占位符可能导致下载进度更新异常。

---

## 6. 手动检查更新

通过 `ShowUpgradeCommand` 在 XAML 中提供“检查更新”入口：

```xaml
<Button
    Command="{h:ShowUpgradeCommand}"
    Content="检查更新" />
```

菜单示例：

```xaml
<MenuItem
    Command="{h:ShowUpgradeCommand}"
    Header="检查更新" />
```

命令会调用：

```csharp
IUpgradeService.Upgrade(out string message)
```

如果没有新版本、清单读取失败或用户取消，命令通过消息服务显示返回信息。

代码中手动检查：

```csharp
using H.Services.Common.Upgrade;

public sealed class UpdateController
{
    private readonly IUpgradeService _upgradeService;

    public UpdateController(IUpgradeService upgradeService)
    {
        _upgradeService = upgradeService;
    }

    public bool CheckAndShow(out string message)
    {
        return _upgradeService.Upgrade(out message);
    }
}
```

只判断是否存在更新：

```csharp
if (_upgradeService.CanUpgrade(out string message))
{
    string version = _upgradeService.UpgradeVersion;
}
```

注意 `CanUpgrade()` 和 `UpgradeVersion` 会分别请求远程清单。连续调用可能产生重复网络请求，默认服务没有缓存版本清单。

---

## 7. 更新检查行为

默认 `UpdateService` 使用 `IWebXmlSerializerService` 获取清单。其主要行为：

- 使用 `HttpClient.GetStringAsync(...).Result` 同步等待网络响应。
- 使用 `XmlSerializer` 反序列化 `UpdateXmlInfo`。
- 禁用 XML 外部解析器。
- 请求或反序列化失败时返回错误消息并记录日志。

由于默认调用是同步的，直接在 UI 线程执行更新检查可能阻塞界面。网络缓慢或服务不可用时，用户可能看到短暂无响应。需要超时、取消或完全异步检查时，应替换 `IUpgradeService` 或 `IWebXmlSerializerService`。

`Uri` 必须是有效的绝对 URI。`XmlWebSerializerService` 在进入异常捕获前创建 `Uri` 对象，因此空值或非法 URI 可能直接抛出异常。

---

## 8. 启动时检查更新

`UpdateService` 实现了 `ISplashLoadable`，其 `Load()` 会读取：

```csharp
UpgradeOptions.Instance.CheckUpdateOnStart
```

但当前 `AddAutoUpgrade()` 只将默认实现注册为 `IUpgradeService`，对应的 `ISplashLoadable` 注册代码处于注释状态。因此，仅设置 `CheckUpdateOnStart = true` 不会自动加入 `ApplicationBase` 的启动加载集合。

如需沿用当前实现并启用启动检查，可增加接口映射：

```csharp
using H.Common.Interfaces;
using H.Services.Common.Upgrade;

services.AddAutoUpgrade(options =>
{
    options.Uri = "https://updates.example.com/MyApp/update.xml";
    options.CheckUpdateOnStart = true;
});

services.AddSingleton<ISplashLoadable>(provider =>
    (ISplashLoadable)provider.GetRequiredService<IUpgradeService>());
```

并在配置阶段显式设置：

```csharp
app.UseUpgrade(options =>
{
    options.Uri = "https://updates.example.com/MyApp/update.xml";
    options.CheckUpdateOnStart = true;
});
```

使用该映射时需要注意：

- 更新检查会进入启动页加载流程。
- 网络检查是同步操作，可能延长启动时间。
- 检查失败可能让启动加载流程返回失败。
- 应确保消息、对话框和启动页能力已经注册。

如果不希望网络问题影响启动，建议在主窗口加载后自行异步调用更新服务，而不是注册为 `ISplashLoadable`。

---

## 9. 更新提示界面

检测到新版本后，默认服务显示 `UpgradePresenter`，窗口默认大小为：

```text
Width: 500
Height: 400
```

界面展示：

- 新版本号。
- 按 `;` 拆分后的更新说明。
- “立即升级”按钮。
- 下载状态文本。
- 下载进度条。

清单示例：

```xml
<changelog>修复问题 A;优化功能 B;增加功能 C</changelog>
```

将显示为三条说明。当前拆分逻辑不自动去除空白或空条目，生成清单时应避免连续分号和多余空格。

---

## 10. 浏览器下载

设置：

```csharp
options.UseIEDownload = true;
```

用户点击“立即升级”后，默认 Presenter 调用：

```csharp
Process.Start(new ProcessStartInfo(downloadUri)
{
    UseShellExecute = true
});
```

这会交给系统默认浏览器处理下载地址。尽管属性名称是 `UseIEDownload`，实际实现并不限定 Internet Explorer，而是使用系统 Shell 的默认关联程序。

浏览器下载模式：

- 不显示内置下载进度。
- 不自动定位下载后的文件。
- 不自动启动安装包。
- 下载和安装流程由浏览器及用户负责。

---

## 11. 内置下载安装

设置：

```csharp
options.UseIEDownload = false;
```

内置流程：

1. 创建 `SavePath` 目录。
2. 从下载 URL 获取文件名。
3. 如果目标文件已存在，询问删除重下或直接安装。
4. 使用 `HttpWebRequest` 下载文件。
5. 更新下载文本和进度。
6. 下载完成后询问是否立即安装。
7. 使用 `Process.Start()` 打开文件。

建议下载地址以明确文件名结尾：

```text
https://updates.example.com/MyApp/MyApp-1.3.1.exe
```

避免使用无法得到有效文件名的地址或带复杂查询参数的临时 URL，因为默认实现直接执行：

```csharp
Path.GetFileName(downloadUri)
```

支持的后缀：

```text
.msi
.exe
.zip
```

对于 `.zip`，默认行为是交给系统关联程序打开，不会自动解压、替换应用文件或重启应用。

启动安装包后，当前应用不会自动退出。安装程序需要覆盖正在使用的文件时，应在自定义更新流程中安排退出、辅助更新进程和重启逻辑。

---

## 12. 当前选项的实际作用

当前默认实现中，各选项实际使用情况如下：

| 选项 | 当前是否使用 | 说明 |
|---|---|---|
| `Uri` | 是 | 获取 XML 清单。 |
| `SavePath` | 是 | 保存下载文件。 |
| `LoadFormat` | 是 | 格式化下载进度。 |
| `UseIEDownload` | 是 | 切换浏览器或内置下载。 |
| `CheckUpdateOnStart` | 部分 | `UpdateService.Load()` 会读取，但默认未注册为启动加载服务。 |
| `AutomaticUpgrade` | 否 | 当前流程不会根据此值自动安装。 |
| `NotifyUpgrade` | 否 | 当前流程不会根据此值切换为仅通知。 |

二次开发时不要仅根据设置项名称推断行为，应以当前 `UpdateService` 和 `UpgradePresenter` 实现为准。

---

## 13. 强制更新说明

XML 模型支持：

```xml
<force>true</force>
```

但当前 `UpdateService.GetVersion()` 创建内部 `VersionData` 时没有把 `UpdateXmlInfo.Force` 赋给 `VersionData.Force`。因此默认运行时的 `Force` 始终为 `false`，强制更新不会按清单生效。

在修复映射或替换服务前，不应依赖 `force` 实现安全更新或强制版本淘汰。

正确映射逻辑应包含：

```csharp
Force = args.Force
```

如果业务必须强制更新，建议实现自定义 `IUpgradeService`，并明确处理：

- 用户拒绝升级时是否退出应用。
- 网络失败时是否允许离线运行。
- 安装包验证失败时的回退策略。
- 更新失败后的恢复机制。

---

## 14. 替换更新服务

`AddAutoUpgrade()` 使用 `TryAdd` 注册默认服务，因此可在调用前注册自定义 `IUpgradeService`：

```csharp
services.AddSingleton<IUpgradeService, MyUpgradeService>();
services.AddAutoUpgrade();
```

自定义服务：

```csharp
using H.Services.Common.Upgrade;

public sealed class MyUpgradeService : IUpgradeService
{
    public string UpgradeVersion { get; private set; }

    public bool CanUpgrade(out string message)
    {
        // 异步请求、缓存清单或查询企业更新服务。
        message = null;
        return false;
    }

    public bool Upgrade(out string message)
    {
        // 展示自定义 UI，验证并启动更新程序。
        message = null;
        return true;
    }
}
```

由于 `IUpgradeService` 当前是同步接口，真正的异步更新体验通常需要：

- 在实现内部管理后台任务。
- 额外定义异步接口。
- 让命令或 ViewModel 调用异步服务。

---

## 15. 替换清单读取服务

`AddAutoUpgrade()` 还会通过 `TryAdd` 注册：

```text
IWebXmlSerializerService -> XmlWebSerializerService
```

需要鉴权、超时、重试或本地缓存时，可以提前注册自定义实现：

```csharp
services.AddSingleton<IWebXmlSerializerService,
    MyWebXmlSerializerService>();
services.AddAutoUpgrade();
```

适合扩展的能力：

- 配置请求超时。
- 使用共享 `HttpClient`。
- 添加认证 Header。
- 支持代理服务器。
- 使用缓存避免重复请求。
- 校验响应 Content-Type。
- 增加遥测和重试策略。

---

## 16. 发布和安全建议

默认模块没有验证安装包内容。生产环境至少应增加：

1. **HTTPS**：清单和安装包都使用可信 HTTPS 地址。
2. **哈希校验**：清单提供 SHA-256，并在启动安装前验证。
3. **数字签名**：验证安装包签名和发布者。
4. **原子发布**：先上传安装包，再发布指向它的清单。
5. **回滚机制**：保留上一稳定版本或恢复程序。
6. **权限控制**：明确安装是否需要管理员权限。
7. **超时和取消**：避免网络异常无限阻塞启动或 UI。
8. **临时文件策略**：下载到临时文件，验证成功后再改为最终文件名。
9. **渠道隔离**：测试、预发布和生产使用不同清单地址。

不要从不受信任的地址读取更新清单，也不要直接执行未验证的下载文件。

默认下载器还存在以下边界：

- 使用同步 `HttpWebRequest`。
- 不支持断点续传。
- 不提供取消令牌。
- 不校验 Content-Length 和最终文件大小。
- 将内容长度转换为 `int`，不适合超大安装包。
- 下载异常后可能遗留不完整文件。
- 下载进度回调可能在后台线程更新绑定属性。

对可靠性要求高的产品建议替换默认下载实现。

---

## 17. 完整配置示例

```csharp
using H.Extensions.ApplicationBase;
using H.Modules.Upgrade;
using H.Services.AppPath;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MyApp;

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddSetting();
        services.AddWindowMessage();
        services.AddWindowDialogMessage();

        services.AddAutoUpgrade(options =>
        {
            options.Uri =
                "https://updates.example.com/MyApp/update.xml";
            options.SavePath = AppPaths.Instance.Cache;
            options.LoadFormat = "正在下载 {0}/{1}";
            options.UseIEDownload = false;
            options.CheckUpdateOnStart = false;
        });
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);

        app.UseUpgrade(options =>
        {
            options.Uri =
                "https://updates.example.com/MyApp/update.xml";
            options.SavePath = AppPaths.Instance.Cache;
            options.LoadFormat = "正在下载 {0}/{1}";
            options.UseIEDownload = false;
            options.CheckUpdateOnStart = false;
        });
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
```

主窗口菜单：

```xaml
<MenuItem
    Command="{h:ShowUpgradeCommand}"
    Header="检查更新" />
```

---

## 18. 常见问题

### 点击“检查更新”后提示地址错误

检查 `UpgradeOptions.Uri` 是否：

- 非空。
- 是有效绝对 URI。
- 可以从客户端网络访问。
- 返回符合 `UpdateXmlInfo` 结构的 XML。

### 一直提示当前已经是最新版本

确认清单版本严格大于：

```csharp
Assembly.GetEntryAssembly().GetName().Version
```

检查项目的 `AssemblyVersion`，不要只查看 UI 中显示的产品版本。

### 清单反序列化失败

确认：

1. 根元素为 `<item>`。
2. 元素名使用小写 `url`、`changelog`、`version`、`force`。
3. `version` 能由 `System.Version` 解析。
4. XML 编码和转义正确。
5. 服务端没有返回登录页、错误页或 JSON。

### 设置了启动检查但没有执行

当前 `AddAutoUpgrade()` 没有默认注册 `ISplashLoadable`。增加接口映射，或在主窗口加载后手动调用 `IUpgradeService.Upgrade()`。

### 设置了 `AutomaticUpgrade` 但仍要求点击按钮

当前默认服务没有使用 `AutomaticUpgrade`。需要自定义 `IUpgradeService` 或更新 Presenter 流程。

### 设置了 `force=true` 但仍可取消

当前 `Force` 没有从 XML 模型映射到内部 `VersionData`。在修复映射或替换更新服务前，强制更新配置不会生效。

### 下载完成后没有自动覆盖程序

默认模块只启动安装包或打开 ZIP，不负责覆盖运行中的应用文件。应使用 MSI/EXE 安装程序，或实现独立更新器处理退出、替换和重启。

### 下载进度显示异常

检查：

- `LoadFormat` 是否包含 `{0}` 和 `{1}`。
- 服务端是否返回有效 `Content-Length`。
- 下载文件是否过大。
- 是否存在后台线程绑定更新问题。

### 安装包无法启动

确认文件扩展名为 `.msi`、`.exe` 或 `.zip`，文件下载完整，当前用户有执行权限，并检查 Windows 安全策略或杀毒软件拦截记录。
