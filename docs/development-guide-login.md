# WPF-Control 二次开发文档 如何配置登录页面的示例
# 登录页面二次开发文档

**适用模块：** `H.Modules.Login`  
**参考示例：** `Source/Tests/H.Test.Login`  
**框架：** `.NET 8.0-windows` + WPF

本文说明如何在应用中启用、配置和扩展登录页面。登录页面由 `H.Modules.Login` 提供，通常配合 `ApplicationBase`、设置模块、消息模块和主题样式使用。

---

## 1. 模块概览

`H.Modules.Login` 提供以下能力：

- 登录页面展示与登录流程控制。
- 注册页面展示与注册流程控制。
- 登录、注册、当前用户、登录按钮等 Presenter。
- 登录配置项持久化与设置页集成。
- 默认测试登录服务，便于快速验证登录流程。
- 与 `ApplicationBase` 的 `OnLogin`、`Login`、`Logout` 流程配合。

常用类型：

| 类型 | 说明 |
|---|---|
| `ILoginService` | 登录服务接口，负责登录、退出和当前用户状态。 |
| `IRegisterService` | 注册服务接口，负责注册逻辑。 |
| `ILoginOptions` | 登录页面配置接口。 |
| `IRegistorOptions` | 注册页面配置接口。 |
| `LoginOptions` | 登录页面配置实现，继承 `IocOptionInstance<LoginOptions>`。 |
| `RegistorOptions` | 注册页面配置实现，继承 `IocOptionInstance<RegistorOptions>`。 |
| `ILoginViewPresenter` | 登录页面 Presenter 接口。 |
| `ILoginButtonViewPresenter` | 登录按钮 Presenter，可嵌入主窗口。 |
| `ICurrentUserViewPresenter` | 当前用户信息 Presenter。 |
| `LoginCommand` | 显示登录页面命令。 |

---

## 2. 项目引用

参考 `H.Test.Login.csproj`，应用至少需要引用登录模块、应用基类、消息模块、设置模块、主题和样式。

```xml
<ItemGroup>
  <ProjectReference Include="..\..\Extensions\H.Extensions.ApplicationBase\H.Extensions.ApplicationBase.csproj" />
  <ProjectReference Include="..\..\Modules\H.Modules.Login\H.Modules.Login.csproj" />
  <ProjectReference Include="..\..\Modules\H.Modules.Messages.Dialog\H.Modules.Messages.Dialog.csproj" />
  <ProjectReference Include="..\..\Modules\H.Modules.Setting\H.Modules.Setting.csproj" />
  <ProjectReference Include="..\..\Styles\H.Style\H.Style.csproj" />
  <ProjectReference Include="..\..\Themes\H.Theme\H.Theme.csproj" />
</ItemGroup>
```

如果启用邮箱注册，还需要邮件扩展：

```xml
<ProjectReference Include="..\..\Extensions\H.Extensions.Mail\H.Extensions.Mail.csproj" />
```

---

## 3. 在 `App.xaml` 中合并主题和样式

`H.Test.Login` 使用 `ApplicationBase` 作为应用入口，并在资源字典中合并主题和样式。

```xaml
<h:ApplicationBase
    x:Class="H.Test.Login.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <FontSizeTheme Type="Default" />
                <LayoutTheme Type="Default" />
                <ColorTheme Type="Dark" />
                <ConciseStyle />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</h:ApplicationBase>
```

说明：

- `FontSizeTheme`：配置默认字号资源。
- `LayoutTheme`：配置默认布局尺寸资源。
- `ColorTheme`：配置颜色主题，示例中使用 `Dark`。
- `ConciseStyle`：加载默认控件样式。

---

## 4. 注册登录相关服务

在 `App.xaml.cs` 中继承 `ApplicationBase`，并在 `ConfigureServices` 注册登录页面、登录服务、注册服务、消息和设置。

```csharp
using H.Extensions.ApplicationBase;
using H.Extensions.Mail;
using H.Modules.Login;
using H.Modules.Setting;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace H.Test.Login;

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSetting();
        services.AddWindowMessage();
        services.AddAdornerDialogMessage();

        // 登录页面 Presenter，按需选择一种。
        // services.AddLoginViewPresenter();
        // services.AddRegisterLoginViewPresenter();
        services.AddBackgroundRigisterLoginViewPresenter();

        // 示例登录/注册服务，真实项目建议替换为自己的实现。
        services.AddTestLoginService();
        services.AddTestRegistorService();

        // 启用邮箱注册时需要注册邮件服务。
        services.AddMail();
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);
        app.UseLoginOptions();
        app.UseRegistorOptions();
        app.UseMailOptions();
    }
}
```

---

## 5. 登录页面 Presenter 选择

`H.Modules.Login` 提供多种登录页面注册方法：

| 方法 | 说明 |
|---|---|
| `AddLoginViewPresenter()` | 标准登录页面。 |
| `AddBackgroundLoginViewPresenter()` | 带背景图的登录页面。 |
| `AddRegisterLoginViewPresenter()` | 标准登录 + 注册页面。 |
| `AddBackgroundRigisterLoginViewPresenter()` | 带背景图的登录 + 注册页面，`H.Test.Login` 使用该方式。 |

示例：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddSetting();
    services.AddWindowMessage();
    services.AddAdornerDialogMessage();

    services.AddBackgroundRigisterLoginViewPresenter();
    services.AddTestLoginService();
    services.AddTestRegistorService();
}
```

> 注意：`Rigister` / `Registor` 是当前项目中已有类型和方法的拼写，使用时应保持与 API 名称一致。

---

## 6. 配置登录页面参数

`LoginOptions` 是登录页面配置入口。可通过两种方式配置：

1. 在服务注册时传入 `setupAction`。
2. 在 `Configure` 中调用 `UseLoginOptions` 配置运行时选项。

推荐在 `UseLoginOptions` 中配置最终显示参数：

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);

    app.UseLoginOptions(option =>
    {
        option.Product = "WPF-Control 示例系统";
        option.ProductFontSize = 42;
        option.AdminName = "admin";
        option.AdminPassword = "123456";
        option.LastUserName = "admin";
        option.LastPassword = "123456";
        option.Remember = true;
        option.UseVisitor = false;
        option.UseLogoutRestart = false;
        option.Background = "pack://application:,,,/H.Modules.Login;component/Assets/background.jpg";
    });

    app.UseRegistorOptions();
}
```

常用配置项：

| 配置项 | 默认值 | 说明 |
|---|---|---|
| `Product` | `ApplicationProvider.Product` | 登录页标题。 |
| `ProductFontSize` | `50` | 登录页标题字体大小。 |
| `AdminName` | `admin` | 默认管理员账号。 |
| `AdminPassword` | `123456` | 默认管理员密码。 |
| `LastUserName` | `admin` | 上次登录账号。 |
| `LastPassword` | `123456` | 上次登录密码。 |
| `Remember` | `true` | 是否记住密码。 |
| `UseVisitor` | `false` | 是否启用访客模式；启用后登录失败也可以进入主窗口。 |
| `UseLogoutRestart` | `false` | 退出登录后是否重启应用。 |
| `Background` | 登录模块内置背景图 | 背景图资源地址。 |

---

## 7. 配置注册页面参数

如果使用 `AddRegisterLoginViewPresenter()` 或 `AddBackgroundRigisterLoginViewPresenter()`，建议同时启用注册配置：

```csharp
protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);

    app.UseRegistorOptions(option =>
    {
        option.UseMail = false;
        option.ServiceAgreementUri = "https://hebiangu.github.io/WPF-Control/ServiceAgreement.html";
        option.PrivacypolicyUri = "https://hebiangu.github.io/WPF-Control/PrivacyPolicy.html";
        option.MailAccount = "example@163.com";
        option.Image = "pack://application:,,,/H.Style;component/Assets/Logo.ico";
    });
}
```

常用配置项：

| 配置项 | 默认值 | 说明 |
|---|---|---|
| `UseMail` | `false` | 是否启用邮箱注册。 |
| `ServiceAgreementUri` | 官方服务协议地址 | 应用许可/服务协议链接。 |
| `PrivacypolicyUri` | 官方隐私策略地址 | 隐私策略链接。 |
| `MailAccount` | 默认邮箱账号 | 发送验证码的邮箱账号。 |
| `Image` | `H.Style` 内置 Logo | 注册页左侧图片。 |

启用邮箱注册时，还需要配置邮件服务：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddMail();
}

protected override void Configure(IApplicationBuilder app)
{
    base.Configure(app);
    app.UseMailOptions(option =>
    {
        // 按邮件扩展的配置项填写 SMTP、账号、密码等。
    });
}
```

---

## 8. 在主窗口中显示登录入口和退出登录

`H.Test.Login` 的 `MainWindow.xaml` 展示了三个常见入口：

- `ShowSettingCommand`：打开设置页面，可查看登录配置项。
- `LogoutCommand`：退出登录。
- `ILoginButtonViewPresenter`：从 IOC 中解析登录按钮 Presenter。

```xaml
<Window
    x:Class="H.Test.Login.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu"
    Title="MainWindow"
    Width="800"
    Height="450"
    Style="{StaticResource {x:Static WindowKeys.Default}}">
    <UniformGrid>
        <FontIconButton
            Command="{ShowSettingCommand}"
            Style="{DynamicResource {x:Static FontIconButtonKeys.Command}}" />

        <Button
            Command="{LogoutCommand}"
            Content="退出登录" />

        <ContentControl Content="{Ioc Type={x:Type ILoginButtonViewPresenter}}" />
    </UniformGrid>
</Window>
```

使用 `ContentControl Content="{Ioc Type={x:Type ILoginButtonViewPresenter}}"` 可以直接把登录按钮嵌入标题栏、工具栏或主界面。

---

## 9. 替换为真实登录服务

`AddTestLoginService()` 注册的是示例登录服务，仅用于测试。真实项目应实现自己的 `ILoginService` 并注册到 IOC。

示例：

```csharp
using H.Modules.Login;
using H.Modules.Login.Base;

public class MyLoginService : BindableBase, ILoginService
{
    private IUser _user;

    public IUser User
    {
        get => _user;
        private set
        {
            _user = value;
            RaisePropertyChanged();
        }
    }

    public bool Login(string name, string password, out string message)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            message = "账号不能为空";
            return false;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            message = "密码不能为空";
            return false;
        }

        // TODO: 替换为数据库、Web API、LDAP 或本地配置校验。
        if (name == "admin" && password == "123456")
        {
            User = new TestUser(name, password, "管理员");
            message = "登录成功";
            return true;
        }

        message = "账号或密码错误";
        return false;
    }

    public bool Logout(out string message)
    {
        User = null;
        message = "已退出登录";
        return true;
    }
}
```

注册自定义服务：

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddBackgroundRigisterLoginViewPresenter();
    services.AddSingleton<ILoginService, MyLoginService>();
}
```

如果需要替换注册逻辑，实现 `IRegisterService`，并注册：

```csharp
services.AddSingleton<IRegisterService, MyRegisterService>();
```

---

## 10. 控制启动时登录流程

`ApplicationBase` 提供登录生命周期入口，常见扩展点：

```csharp
protected override void OnSplashScreen(StartupEventArgs e)
{
    base.OnSplashScreen(e);
}

protected override void OnLogin()
{
    base.OnLogin();
    // 登录前后需要执行的逻辑可放在这里。
}
```

主窗口内也可以通过命令触发登录/退出：

```xaml
<Button Command="{LogoutCommand}" Content="退出登录" />
<ContentControl Content="{Ioc Type={x:Type ILoginButtonViewPresenter}}" />
```

---

## 11. 完整最小配置示例

### `App.xaml`

```xaml
<h:ApplicationBase
    x:Class="MyApp.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <FontSizeTheme Type="Default" />
                <LayoutTheme Type="Default" />
                <ColorTheme Type="Dark" />
                <ConciseStyle />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</h:ApplicationBase>
```

### `App.xaml.cs`

```csharp
using H.Extensions.ApplicationBase;
using H.Modules.Login;
using H.Modules.Setting;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MyApp;

public partial class App : ApplicationBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSetting();
        services.AddWindowMessage();
        services.AddAdornerDialogMessage();
        services.AddBackgroundRigisterLoginViewPresenter();
        services.AddTestLoginService();
        services.AddTestRegistorService();
    }

    protected override void Configure(IApplicationBuilder app)
    {
        base.Configure(app);
        app.UseLoginOptions(option =>
        {
            option.Product = "MyApp";
            option.AdminName = "admin";
            option.AdminPassword = "123456";
            option.Remember = true;
        });
        app.UseRegistorOptions();
    }

    protected override Window CreateMainWindow(StartupEventArgs e)
    {
        return new MainWindow();
    }
}
```

### `MainWindow.xaml`

```xaml
<Window
    x:Class="MyApp.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:h="https://github.com/HeBianGu"
    Width="800"
    Height="450"
    Style="{StaticResource {x:Static WindowKeys.Default}}">
    <StackPanel>
        <ContentControl Content="{Ioc Type={x:Type ILoginButtonViewPresenter}}" />
        <Button Command="{LogoutCommand}" Content="退出登录" />
    </StackPanel>
</Window>
```

---

## 12. 常见问题

### 登录页面不显示

检查：

1. `App` 是否继承 `ApplicationBase`。
2. 是否注册了 `AddLoginViewPresenter()`、`AddRegisterLoginViewPresenter()` 或 `AddBackgroundRigisterLoginViewPresenter()`。
3. 是否注册了 `ILoginService`，例如 `AddTestLoginService()` 或自定义服务。
4. 是否调用了 `base.Configure(app)`。

### 登录按钮无法显示

检查是否注册了登录 Presenter。`AddLoginViewPresenter()`、`AddRegisterLoginViewPresenter()` 等方法内部会注册 `ILoginButtonViewPresenter`。

### 设置页看不到登录配置

检查是否注册并启用了设置：

```csharp
services.AddSetting();
app.UseLoginOptions();
app.UseRegistorOptions();
```

### 注册页面邮箱功能不可用

检查：

1. 是否使用 `AddRegisterLoginViewPresenter()` 或 `AddBackgroundRigisterLoginViewPresenter()`。
2. 是否设置 `RegistorOptions.UseMail = true`。
3. 是否注册 `services.AddMail()` 并调用 `app.UseMailOptions()`。
4. 邮件服务的 SMTP、账号和授权码是否配置正确。

### 退出登录后行为不符合预期

检查 `LoginOptions.UseLogoutRestart`：

- `false`：退出登录后只清空当前用户。
- `true`：退出登录后重启应用。

